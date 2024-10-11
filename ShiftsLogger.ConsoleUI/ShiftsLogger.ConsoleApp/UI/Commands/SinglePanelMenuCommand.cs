using System.Collections.Immutable;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.Services;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.LayoutComposition;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.UI.Commands;

public abstract class SinglePanelMenuCommand : ICommand
{
    protected readonly IPanelBuilderService PanelBuilderService;
    protected readonly IRenderService RenderService;
    protected ImmutableList<IViewModelEntity> MenuEntries;
    protected bool IsMenuRunning;

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
            MenuEntries = PopulateMenuEntries();
        }
        
        IsMenuRunning = true;
        var viewModel = new SinglePanelViewModel(MenuEntries);
        var selectionService = GetSelectionService(viewModel);
        var layoutComposer = GetLayoutComposer();

        while (IsMenuRunning)
        {
            var panel = PanelBuilderService.CreatePanel(
                menuRepresentation: (entry) => entry.GetShortRepresentation(), // TODO: Make appropriate in menu view
                viewModel: viewModel, 
                textAlignment: HorizontalAlignment.Center,
                color: Color.Green,
                isSinglePanel: true,
                TextDecorations.Underline, TextDecorations.Bold 
            );
            
            layoutComposer.SetRenderables(panel);
            var layout = layoutComposer.GetLayout();
            
            RenderService.Render(layout);
            
            ProcessUserInput(selectionService, viewModel);
        }
    }

    protected abstract object GetChosenOption (ISinglePanelViewModel viewModel);
    
    protected abstract ICommand GetCommand(object chosenOption);
    
    protected abstract ImmutableList<IViewModelEntity> PopulateMenuEntries();

    protected virtual ISelectionService GetSelectionService(ISinglePanelViewModel viewModel) =>
        new SelectionService(new SinglePanelSelectionStrategy(viewModel));

    protected virtual LayoutComposerService GetLayoutComposer() =>
        new(new SinglePanelLayoutStrategy());

    protected virtual void ProcessUserInput(ISelectionService selectionService, ISinglePanelViewModel viewModel)
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