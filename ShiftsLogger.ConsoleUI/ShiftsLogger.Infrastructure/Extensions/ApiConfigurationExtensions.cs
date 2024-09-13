using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Configurations;
using ShiftsLogger.Infrastructure.Mappers;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class ApiConfigurationExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("ApiConfig"));
        services.AddApiEndpointMapper();
    }
    
    private static void AddApiEndpointMapper(this IServiceCollection services) =>
        services.AddScoped<IApiEndpointMapper, ApiEndpointMapper>();
}