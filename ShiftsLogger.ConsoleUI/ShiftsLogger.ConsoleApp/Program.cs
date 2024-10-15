using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp;

public static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var renderService = serviceProvider.GetRequiredService<IRenderService>();
        var panelBuilder = serviceProvider.GetRequiredService<IPanelBuilderService>();
        var mainMenuFactory = serviceProvider.GetRequiredService<ICommandFactory<MainMenuOptions>>();
        
        var showMainMenu = new ShowMainMenu(renderService, panelBuilder, mainMenuFactory);
        
        showMainMenu.Execute();
    }
}