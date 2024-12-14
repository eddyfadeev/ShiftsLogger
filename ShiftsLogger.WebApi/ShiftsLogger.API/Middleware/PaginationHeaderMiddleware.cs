using System.Text.Json;
using Shared.RequestFeatures;

namespace ShiftsLogger.API.Middleware;

public class PaginationHeaderMiddleware
{
    private readonly RequestDelegate _next;
    
    public PaginationHeaderMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Items.TryGetValue(nameof(PaginationMetaData), out var paginationData);
            
            if (paginationData is PaginationMetaData)
            {
                context.Response.Headers["X-Pagination"] = JsonSerializer.Serialize(paginationData);

            }
            
            return Task.CompletedTask;
        });
        
        await _next(context);
    }
}