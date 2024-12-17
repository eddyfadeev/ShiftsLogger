using Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShiftsLogger.ConsoleApp.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApiEndpoints(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<ApiEndpointsOptions>(configuration.GetSection(ApiEndpointsOptions.ApiEndpoints));
}