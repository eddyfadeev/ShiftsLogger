using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Commands;

public abstract class SinglePanelMenuCommand<TEntry> : ICommand
    where TEntry : class
{
    private protected readonly IPanelBuilder PanelBuilder;
    private protected readonly IRenderService RenderService;
    private protected List<TEntry> MenuEntries;
    private protected bool IsMenuRunning;

    protected SinglePanelMenuCommand(
        IRenderService renderService,
        IPanelBuilder panelBuilder)
    {
        RenderService = renderService;
        PanelBuilder = panelBuilder;
        MenuEntries = [];
    }

    public virtual void Execute()
    {
        IsMenuRunning = true;
        var viewModel = new SinglePanelViewModel<TEntry>(MenuEntries);
        var selectionService = GetSelectionService(viewModel);

        while (IsMenuRunning)
        {
            var panel = PanelBuilder.CreatePanel(
                renderInfo: viewModel, 
                textAlignment: HorizontalAlignment.Center,
                color: Color.Green,
                isSinglePanel: true,
                textDecorations: [ TextDecorations.Underline, TextDecorations.Bold ]
            );
            RenderService.RenderSinglePanelLayout(panel);
            
            ProcessUserInput(selectionService, viewModel);
        }
    }

    private protected abstract object GetChosenOption (SinglePanelViewModel<TEntry> viewModel);
    
    private protected abstract ICommand GetCommand(object chosenOption);
    
    private protected abstract List<TEntry> PopulateMenuEntries();
    
    private protected virtual SelectionService GetSelectionService(SinglePanelViewModel<TEntry> viewModel) =>
        new(new SinglePanelSelectionStrategy<TEntry>(viewModel));
    
    private protected virtual void ProcessUserInput(SelectionService selectionService, SinglePanelViewModel<TEntry> viewModel)
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