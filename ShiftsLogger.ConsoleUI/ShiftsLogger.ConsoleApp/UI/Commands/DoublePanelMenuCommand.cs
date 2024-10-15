using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.LayoutComposition;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.ConsoleApp.UI.Commands;

public abstract class DoublePanelMenuCommand : ICommand
{
    protected readonly IRenderService RenderService;
    protected readonly IPanelBuilderService PanelBuilderService;
    
    protected bool IsMenuRunning; 
    protected IEnumerable<IViewModelEntity> LeftPanelEntities;
    protected IEnumerable<IViewModelEntity> RightPanelEntries;

    protected DoublePanelMenuCommand(IRenderService renderService, IPanelBuilderService panelBuilderService)
    {
        RenderService = renderService;
        PanelBuilderService = panelBuilderService;
        
        LeftPanelEntities = [];
        RightPanelEntries = [];
    }
    
    public virtual void Execute()
    {
        if (!LeftPanelEntities.Any())
        {
            LeftPanelEntities = FetchLeftPanelData();
        }
        
        var viewModel = CreatePrimaryViewModel();
        var selectionService = GetSelectionService(viewModel);
        var layoutComposer = GetLayoutComposer();
        
        IsMenuRunning = true;
        
        GetRenderables(viewModel, out var leftPanel, out var rightPanel);
        layoutComposer.SetRenderables(leftPanel, rightPanel);
        var layout = layoutComposer.GetLayout();
        
        RenderService.Render(layout);

        while (IsMenuRunning)
        {
            ProcessUserInput(selectionService, (DoublePanelViewModel)viewModel);
            
            GetRenderables(viewModel, out leftPanel, out rightPanel);
            layoutComposer.SetRenderables(leftPanel, rightPanel);
            layout = layoutComposer.GetLayout();
            
            RenderService.Render(layout);
        }
    }

    protected abstract IEnumerable<IViewModelEntity> FetchLeftPanelData();
    
    protected abstract IEnumerable<IViewModelEntity> FetchRightPanelData(int entityId);
    
    protected virtual IDoublePanelViewModel CreatePrimaryViewModel() => 
        new DoublePanelViewModel(LeftPanelEntities);

    protected virtual ISinglePanelViewModel CreateSecondaryViewModel() =>
        new SinglePanelViewModel(RightPanelEntries);

    protected virtual SelectionService GetSelectionService(
        IDoublePanelViewModel viewModel) =>
        new(
            new DoublePanelSelectionStrategy(viewModel)
            );

    protected virtual LayoutComposerService GetLayoutComposer() =>
        new (new DoublePanelLayoutStrategy(splitRatio: 30, LayoutSplit.Vertical));

    protected virtual void GetRenderables(
        IDoublePanelViewModel viewModel, 
        out IRenderable leftPanel, out IRenderable rightPanel)
    {
        leftPanel =
            PanelBuilderService.CreatePanel(
                menuRepresentation: (entry) => entry.GetShortRepresentation(), 
                viewModel: viewModel.LeftPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                selectorColor: Color.Green,
                isSinglePanel: viewModel.IsSinglePanelMode,
                TextDecorations.Bold, TextDecorations.Underline
            );

        rightPanel = GetRightRenderable(viewModel);
    }

    protected virtual IRenderable GetRightRenderable(IDoublePanelViewModel viewModel) =>
        viewModel switch
        {
            { SelectedFilterIndex: < 0 } => 
                PanelBuilderService.CreateDummyPanel(
                "[grey]Choose a filter to see available shifts...[/]"),
            { SelectedFilterIndex: >= 0, RightPanelViewModel.PanelEntries.VisibleElements.Count: 0 } => 
                PanelBuilderService.CreateDummyPanel(
                "[grey]No shifts available for this filter...[/]"),
            _ => 
                PanelBuilderService.CreatePanel(
                menuRepresentation: (entry) => entry.GetDetailedRepresentation(),
                viewModel: viewModel.RightPanelViewModel,
                textAlignment: HorizontalAlignment.Left, 
                selectorColor: Color.Blue, 
                isSinglePanel: !viewModel.IsSinglePanelMode,
                TextDecorations.Bold, TextDecorations.Underline)
        };

    protected virtual void ProcessUserInput(SelectionService selectionService, IDoublePanelViewModel viewModel)
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
            case ConsoleKey.LeftArrow:
                selectionService.ChangeSelection(Selection.MoveLeft);
                break;
            case ConsoleKey.RightArrow:
                selectionService.ChangeSelection(Selection.MoveRight);
                break;
            case ConsoleKey.Enter:
                selectionService.ChangeSelection(Selection.Select);
                RightPanelEntries = FetchRightPanelData(viewModel.SelectedFilterIndex);
                var rightPanel = CreateSecondaryViewModel();
                viewModel.UpdateRightPanelViewModel(rightPanel);
                break;
            case ConsoleKey.Escape:
                IsMenuRunning = false;
                break;
        }
    }
}