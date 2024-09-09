using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Net;

namespace ShiftsLogger.API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            _logger.LogError(
                ex, 
                "An unexpected error occurred. CorrelationId: {CorrelationId}, RequestPath: {RequestPath}, ErrorMessage: {ErrorMessage}", 
                correlationId, 
                context.Request.Path, 
                ex.Message
            );
            
            await HandleExceptionAsync(context, ex, correlationId);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
    {
        context.Response.ContentType = "application/json";
        
        
        (int statusCode, string message, string details) = GetExceptionDetails(exception);

        LogException(exception, message, details);

        context.Response.StatusCode = statusCode;

        bool includeDetails = context.RequestServices
            .GetRequiredService<IHostEnvironment>()
            .IsDevelopment();
        
        object response = PrepareResponseObject(
            message, 
            details, 
            correlationId,
            includeDetails ? exception.StackTrace : null
            );

        await context.Response.WriteAsJsonAsync(response);
    }
    
    private static (int statusCode, string message, string details) GetExceptionDetails(Exception exception) =>
        exception switch
        {
            BadHttpRequestException badHttpRequestEx => 
                ((int)HttpStatusCode.BadRequest, "Bad HTTP request.", badHttpRequestEx.Message),
            ValidationException valEx =>
                ((int)HttpStatusCode.BadRequest, "Validation failed.", valEx.Message),
            ArgumentNullException nullEx => 
                ((int)HttpStatusCode.BadRequest, "Null is forbidden.", nullEx.Message),
            ArgumentException argEx => 
                ((int)HttpStatusCode.BadRequest, "Invalid argument provided.", argEx.Message),
            KeyNotFoundException keyEx => 
                ((int)HttpStatusCode.NotFound, "Resource not found.", keyEx.Message),
            DbException dbEx => 
                ((int)HttpStatusCode.InternalServerError, "Problem with database", dbEx.Message),
            DataException dex => 
                ((int)HttpStatusCode.BadRequest, "Operation error.", dex.Message),
            InvalidOperationException invOpEx => 
                ((int)HttpStatusCode.BadRequest, "Invalid operation", invOpEx.Message),
            _ => 
                ((int)HttpStatusCode.InternalServerError, "Internal Server Error", exception.Message)
        };

    private void LogException(Exception exception, string explanationMessage, string exceptionMessage)
    {
        switch (exception)
        {
            case BadHttpRequestException: 
            case ArgumentNullException:
            case ArgumentException:
                _logger.LogWarning("{ErrorExplanation} {ExceptionMessage}", explanationMessage, exceptionMessage);
                break;
            case DbException: 
            case InvalidOperationException:
                _logger.LogError("{ErrorExplanation} {ExceptionMessage}", explanationMessage, exceptionMessage);
                break;
            default:
                _logger.LogCritical("{ErrorExplanation} {ExceptionMessage}", explanationMessage, exceptionMessage);
                break;
        }
    }

    private static object PrepareResponseObject(
        string message, 
        string details, 
        string correlationId,
        string? stackTrace = null) => 
        new
        {
            message,
            details,
            correlationId,
            Timestamp = DateTime.UtcNow,
            stackTrace
        };
}