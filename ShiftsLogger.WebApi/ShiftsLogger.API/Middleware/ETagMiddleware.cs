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
            if (context.Request.Method.Equals("GET") && 
                context.Response.StatusCode == 200 )
            {
                var etag = $"\"{Guid.NewGuid():n}\"";
                context.Response.Headers.ETag = etag;
            }
            
            return Task.CompletedTask;
        });
        
        await _next(context);
    }
}