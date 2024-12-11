namespace ShiftsLogger.API.Middleware;

public class ETagMiddleware
{
    private readonly RequestDelegate _next;
    
    public ETagMiddleware(RequestDelegate next) =>
        _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            var etag = $"\"{Guid.NewGuid():n}\"";
            context.Response.Headers.ETag = etag;
            
            return Task.CompletedTask;
        });
        
        await _next(context);
    }
}