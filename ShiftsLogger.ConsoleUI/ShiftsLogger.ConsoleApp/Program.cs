using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.ConsoleApp.Commands.MainMenu;
using ShiftsLogger.ConsoleApp.ConsoleUI;
using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.Infrastructure.Extensions;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp;

public static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        var locationsController = serviceProvider.GetRequiredService<LocationsController>();

        var renderService = serviceProvider.GetRequiredService<IRenderService>();
        var panelBuilder = serviceProvider.GetRequiredService<IPanelBuilderService>();

        #region SinglePanel

        var menuEntries = EnumExtensions.GetDescriptions<MainMenuOptions>().ToList();

        var mainMenuView = new SinglePanelViewModel<string>(menuEntries);
        var singlePanelSelectionService = new SelectionService(new SinglePanelSelectionStrategy<string>(mainMenuView));

        #endregion

        #region DoublePanel

        var shiftsByLocationCommand = new ShiftsByLocationCommand(renderService, panelBuilder, locationsController);
        #endregion
        
        shiftsByLocationCommand.Execute();
        
        // while (true)
        // {
        //     //var panels = panelBuilder.PrepareRenderablePanels(shiftsView);
        //     //panels.Deconstruct(out Panel leftPanel, out Panel rightPanel);
        //
        //     var panel = panelBuilder.PrepareRenderablePanel(mainMenuView, HorizontalAlignment.Center);
        //     renderService.RenderSinglePanelLayout(panel);
        //     
        //     //renderService.RenderDoublePanelLayout(leftPanel, rightPanel);
        //     var key = Console.ReadKey(true).Key;
        //
        //     HandleUserInput(singlePanelSelectionService, key);
        // }
    }
}