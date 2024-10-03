using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.Services;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.UI.Commands;

public abstract class SinglePanelMenuCommand : ICommand
{
    private protected readonly IPanelBuilderService PanelBuilderService;
    private protected readonly IRenderService RenderService;
    private protected List<IViewModelEntity> MenuEntries;
    private protected bool IsMenuRunning;

    protected SinglePanelMenuCommand(
        IRenderService renderService,
        IPanelBuilderService panelBuilderService)
    {
        RenderService = renderService;
        PanelBuilderService = panelBuilderService;
        MenuEntries = [];
    }

    public virtual void Execute()
    {
        if (MenuEntries.Count == 0)
        {
            MenuEntries = PopulateMenuEntries().ToList();
        }
        
        IsMenuRunning = true;
        var viewModel = new SinglePanelViewModel(MenuEntries);
        var selectionService = GetSelectionService(viewModel);

        while (IsMenuRunning)
        {
            var panel = PanelBuilderService.CreatePanel(
                viewModel: viewModel, 
                textAlignment: HorizontalAlignment.Center,
                color: Color.Green,
                isSinglePanel: true,
                textDecorations: [ TextDecorations.Underline, TextDecorations.Bold ]
            );
            RenderService.RenderSinglePanelLayout(panel);
            
            ProcessUserInput(selectionService, viewModel);
        }
    }

    private protected abstract object GetChosenOption (ISinglePanelViewModel viewModel);
    
    private protected abstract ICommand GetCommand(object chosenOption);
    
    private protected abstract IEnumerable<IViewModelEntity> PopulateMenuEntries();

    private protected virtual ISelectionService GetSelectionService(ISinglePanelViewModel viewModel) =>
        new SelectionService(new SinglePanelSelectionStrategy(viewModel));

    private protected virtual void ProcessUserInput(ISelectionService selectionService, ISinglePanelViewModel viewModel)
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
                IsMenuRunning = false;
                break;
        }
    }
}