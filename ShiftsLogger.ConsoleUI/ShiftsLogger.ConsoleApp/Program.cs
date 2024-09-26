using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
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
        
        var locations = locationsController.GetAllLocations().Result;
        var shiftsByLocation = new Dictionary<Location, List<Shift>>();
        foreach (var location in locations)
        {
            shiftsByLocation.Add(
                location, locationsController.GetShiftsByLocationId(location.Id).Result);
        }

        var shiftsView = new DoublePanelViewModel<Location, Shift>(locations, shiftsByLocation);
        var selectionService = new SelectionService(new DoublePanelSelectionStrategy<Location, Shift>(shiftsView));
        
        #endregion
        
        
        while (true)
        {
            //var panels = panelBuilder.PrepareRenderablePanels(shiftsView);
            //panels.Deconstruct(out Panel leftPanel, out Panel rightPanel);

            var panel = panelBuilder.PrepareRenderablePanel(mainMenuView);
            renderService.RenderSinglePanelLayout(panel);
            
            //renderService.RenderDoublePanelLayout(leftPanel, rightPanel);
            var key = Console.ReadKey(true).Key;

            HandleUserInput(singlePanelSelectionService, key);
        }
    }

    private static void HandleUserInput(SelectionService selectionService, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectionService.ChangeSelection(Selection.MoveUp);
                break;
            case ConsoleKey.DownArrow:
                selectionService.ChangeSelection(Selection.MoveDown);
                break;
            case ConsoleKey.LeftArrow:
                selectionService.ChangeSelection(Selection.MoveLeft);
                break;
            case ConsoleKey.RightArrow:
                selectionService.ChangeSelection(Selection.MoveRight);
                break;
            case ConsoleKey.Enter:
                selectionService.ChangeSelection(Selection.Select);
                break;
        }
    }
}