namespace ShiftsLogger.API.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "ShiftsLogger API",
                Description = "A simple API to log worked shifts using ASP.NET Core Web API",
            });
        });
}