using Contracts;
using Entity;
using LoggerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;

namespace ShiftsLogger.ConsoleApp.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApiEndpoints(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<ApiEndpointsOptions>(configuration.GetSection(ApiEndpointsOptions.ApiEndpoints));

    public static void ConfigureLogger(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureApiManager(this IServiceCollection services) =>
        services.AddScoped<IApiServiceManager, ApiServiceManager>();
}