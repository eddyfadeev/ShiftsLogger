using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Extensions;
using ShiftsLogger.Infrastructure.Mappers;

namespace ShiftsLogger.ConsoleApp;

public static class ServicesConfigurator
{
    private const string JsonFileName = "appsettings.json";
    private static readonly IConfiguration Configuration = GetConfigurationBuilder();
    
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton(Configuration);
        services.ConfigureApi(Configuration);
        services.AddTransient<IApiEndpointMapper, ApiEndpointMapper>();
    }

    private static IConfiguration GetConfigurationBuilder() =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(JsonFileName, optional: false, reloadOnChange: true)
            .Build();
}