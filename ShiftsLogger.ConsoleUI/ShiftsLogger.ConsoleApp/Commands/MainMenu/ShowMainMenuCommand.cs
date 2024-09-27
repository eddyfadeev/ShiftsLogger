using System.Collections.Frozen;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Extensions;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShowMainMenuCommand : ICommand
{
    private readonly IRenderService _renderService;
    private readonly IPanelBuilderService _panelBuilderService;
    private readonly ICommandFactory<MainMenuOptions> _mainMenuCommandFactory;
    
    private bool _isMenuRunning;
    private readonly List<string> _menuEntries;
    private FrozenDictionary<string, MainMenuOptions> _mainMenuOptionsMap;

    public ShowMainMenuCommand(
        IRenderService renderService, 
        IPanelBuilderService panelBuilderService, 
        ICommandFactory<MainMenuOptions> mainMenuCommandFactory
        )
    {
        _renderService = renderService;
        _panelBuilderService = panelBuilderService;
        _mainMenuCommandFactory = mainMenuCommandFactory;

        _menuEntries = PopulateMenuEntries();
        _mainMenuOptionsMap = MapMenuOptions();
        _isMenuRunning = true;
    }
    
    public void Execute()
    {
        var viewModel = new SinglePanelViewModel<string>(_menuEntries);
        var selectionService = GetSelectionService(viewModel);

        while (_isMenuRunning)
        {
            var panel = _panelBuilderService.PrepareRenderablePanel(viewModel, HorizontalAlignment.Center);
            _renderService.RenderSinglePanelLayout(panel);
            
            ProcessUserInput(selectionService, viewModel);
        }
    }
    
    private void ProcessUserInput(SelectionService selectionService, SinglePanelViewModel<string> viewModel)
    {
        var key = Console.ReadKey(true).Key;
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                selectionService.ChangeSelection(Selection.MoveUp);
                break;
            case ConsoleKey.DownArrow:
                selectionService.ChangeSelection(Selection.MoveDown);
                break;
            case ConsoleKey.Enter:
                var chosenOption = GetChosenOption(viewModel);
                var submenu = GetCommand(chosenOption);
                submenu.Execute();
                break;
            case ConsoleKey.Escape:
                _isMenuRunning = false;
                break;
        }
    }

    private static List<string> PopulateMenuEntries() =>
        EnumExtensions.GetDescriptions<MainMenuOptions>().ToList();
    
    private static FrozenDictionary<string, MainMenuOptions> MapMenuOptions()
    {
        var mainMenuOptionsMap = new Dictionary<string, MainMenuOptions>();
        
        foreach (var value in Enum.GetValues<MainMenuOptions>())
        {
            mainMenuOptionsMap.Add(value.GetDescription(), value);
        }

        return mainMenuOptionsMap.ToFrozenDictionary();
    }

    private static SelectionService GetSelectionService(SinglePanelViewModel<string> viewModel) =>
        new(new SinglePanelSelectionStrategy<string>(viewModel));

    private MainMenuOptions GetChosenOption(SinglePanelViewModel<string> viewModel)
    {
        string selectedEntry = viewModel.PanelEntries[viewModel.SelectedEntryIndex];
        
        return _mainMenuOptionsMap[selectedEntry];
    }

    private ICommand GetCommand(MainMenuOptions chosenOption) => _mainMenuCommandFactory.Create(chosenOption);
}