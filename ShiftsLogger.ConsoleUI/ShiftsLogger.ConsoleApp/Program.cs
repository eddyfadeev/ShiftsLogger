using Contracts;
using LoggerService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Services;
using Services.Contracts;
using ShiftsLogger.ConsoleApp.Extensions;
using ShiftsLogger.Presentation;
using Spectre.Console;
using ViewConfigurator;

namespace ShiftsLogger.ConsoleApp;

public static class Program
{
    private const string EndpointsConfiguration = "endpoints.json";

    static async Task Main(string[] args)
    {
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        var configuration = BuildConfiguration();
        var services = new ServiceCollection();

        services.ConfigureApiEndpoints(configuration);
        services.ConfigureLogger();
        services.AddHttpClient();
        services.ConfigureApiManager();
        services.AddView();
        services.AddSingleton<IMenuHandler, MenuHandler>();

        var serviceProvider = services.BuildServiceProvider();
        
        Console.CursorVisible = false;

        var logger = serviceProvider.GetRequiredService<ILoggerManager>();
        var menuHandler = serviceProvider.GetRequiredService<IMenuHandler>();
        
        try
        {
            var mainMenu = new MainMenu(serviceProvider);
            menuHandler.PushMenu(mainMenu);

            await menuHandler.RunAsync();

//             var sortKeys = renderData.TableColumnHeaders.Select((name, index) => $"({index + 1}) - {name.OriginString}");
//
//             var footer = $"""
//                           [white]
//                           Press ESC to return to previous menu
//                           Press right arrow to view next page or left arrow to return to the previous page
//                           Press (key) to toggle sorting
//                           {string.Join(" | ", sortKeys)}
//                           [/]
//                           """;
        }
        catch (Exception ex)
        {
            logger.LogException(ex);
            AnsiConsole.WriteException(ex);
        }
        
        
    }

private static IConfiguration BuildConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(EndpointsConfiguration, optional: false, reloadOnChange: true)
            .Build();
}