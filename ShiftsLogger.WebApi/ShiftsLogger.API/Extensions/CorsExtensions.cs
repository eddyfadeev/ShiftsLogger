namespace ShiftsLogger.API.Extensions;

public static class CorsExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()   // Can use as WithOrigins("https://example.com")
                    .AllowAnyMethod()   // Can change to WithMethods("POST", "GET")
                    .AllowAnyHeader()); // Can configure headers WithHeaders("accept", "content-type")
        });
}