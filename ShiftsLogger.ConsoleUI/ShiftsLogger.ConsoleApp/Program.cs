using Contracts;
using LoggerService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Service.Contracts;
using ShiftsLogger.ConsoleApp.Extensions;
using Spectre.Console;
using View.Contracts.Services;
using View.Entity.Structures;
using View.Menu;
using View.TableBuilderService;

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
        services.AddTransient<ITableBuilder, TableBuilder>();

        var serviceProvider = services.BuildServiceProvider();

        var tableBuilder = serviceProvider.GetRequiredService<ITableBuilder>();
        var logger = serviceProvider.GetRequiredService<ILoggerManager>();
        var apiService = serviceProvider.GetRequiredService<IApiServiceManager>();

        var shifts = await apiService.Shift.GetAllShiftsAsync();
        

        try
        {
            Console.CursorVisible = false;
            
            List<string> menuStrings = ["Mange Shifts", "Manage Locations", "Manage Users", "Manage Shift Types"];
            var menuEntries = menuStrings.Select(str => new MenuEntry(str)).ToList();
            var menuSettings = new MenuSettings();
            var mainMenu = new NavigableMenu(tableBuilder, menuSettings, menuEntries);

            while (true)
            {
                mainMenu.DisplayMenu();

                var pressedKey = Console.ReadKey(true);
                
                if (pressedKey.Key == ConsoleKey.Escape)
                    break;
                
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        mainMenu.SelectPrevious();
                        break;
                    case ConsoleKey.DownArrow:
                        mainMenu.SelectNext();
                        break;
                    case ConsoleKey.Enter:
                    {
                        var selectedEntry = menuEntries[mainMenu.MenuTable.SelectedIndex];
                        selectedEntry.Action?.Invoke();
                        break;
                    }
                }
                
                Console.Clear();
            }


//             var sortKeys = tableData.TableColumnHeaders.Select((name, index) => $"({index + 1}) - {name.OriginString}");
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