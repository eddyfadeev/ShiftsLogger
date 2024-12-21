using System.Net;
using Contracts;

namespace LoggerService.Extensions;

public static class LoggerManagerExtensions
{
    public static void LogException(this ILoggerManager logger, Exception exception) =>
        logger.LogError($"""
                         Exception Caught:
                         [{exception.GetType()}]
                         [Exception Message] 
                         {exception.Message}
                         [Inner Exception] 
                         {exception.InnerException?.Message ?? "None"}
                         [Stack Trace] 
                         {exception.StackTrace}
                         """);

    #region Api Transaction using HttClient

    public static void LogDefaultEndpoint(this ILoggerManager logger, string clientName, Uri endpoint) =>
        logger.LogInfo($"[{clientName}] Default endpoint: {endpoint}");
    
    public static void LogApiTransactionStart(this ILoggerManager logger, string methodName, Uri endpoint)
    {
        logger.LogWarn($"[{methodName}] Starting API transaction");
        logger.LogInfo($"[{methodName}] Received request uri: {endpoint}");
        logger.LogInfo($"[{methodName}] Sending request to API");
    }

    public static void LogStatusCode(this ILoggerManager logger, string methodName, HttpStatusCode statusCode) =>
        logger.LogInfo($"[{methodName}] Status code: {statusCode}({(int)statusCode})");
    
    public static void LogApiCallContent<TEntity>
        (this ILoggerManager logger, string methodName, params List<TEntity>? entities)
    {
        logger.LogInfo($"[{methodName}] Obtained result from the API call. Entities #: {entities?.Count}");
        
        if (entities is { Count: <= 0 })
        {
            logger.LogDebug("Received content is null or empty");
        }
        else
        {
            foreach (var entity in entities!)
            {
                logger.LogDebug($"[{entity?.GetType()}]");
                logger.LogDebug($"{entity?.ToString()}");
            }
            
        }
    }

    public static void LogApiCallHeaders
        (this ILoggerManager logger, string methodName, HttpResponseMessage responseMessage)
    {
        logger.LogInfo($"[{methodName}] {nameof(HttpResponseMessage)} contains " +
                       $"{responseMessage.Headers.Count()} headers from the API call");

        foreach (var kvp in responseMessage.Headers)
        {
            logger.LogDebug($"[{kvp.Key}]: [{string.Join(", ", kvp.Value)}]");
        }
        
        logger.LogInfo($"[{methodName}] {nameof(HttpResponseMessage.Content)} contains " +
                       $"{responseMessage.Content.Headers.Count()} headers from the API call");

        foreach (var kvp in responseMessage.Content.Headers)
        {
            logger.LogDebug($"[{kvp.Key}]: [{string.Join(", ", kvp.Value)}]");
        }
    }

    public static void LogPostData<TEntity>(this ILoggerManager logger, string methodName, TEntity? entity)
    {
        logger.LogInfo($"[{methodName}] Posting data to the API");
        logger.LogDebug($"[{methodName}] {entity?.ToString() ?? "Entity is null"}");
    }

    public static void LogApiTransactionEnd(this ILoggerManager logger, string methodName) =>
        logger.LogWarn($"[{methodName}] End of transaction");

    public static void LogEntityId(this ILoggerManager logger, string methodName, string entityName, Guid id) =>
        logger.LogInfo($"[{methodName}] Processing {entityName} id: {id}");

    public static void LogPassedObject(this ILoggerManager logger, string methodName, object? obj) =>
        logger.LogWarn($"[{methodName}] Passed object is {obj?.GetType().Name ?? "null"}");

    #endregion
}