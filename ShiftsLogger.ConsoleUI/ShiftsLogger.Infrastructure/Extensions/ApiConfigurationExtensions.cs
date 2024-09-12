using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Infrastructure.Configurations;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class ApiConfigurationExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("ApiConfig"));
    }
}