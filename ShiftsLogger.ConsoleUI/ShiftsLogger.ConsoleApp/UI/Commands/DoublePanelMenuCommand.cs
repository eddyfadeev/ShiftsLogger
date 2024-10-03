using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.UI.Commands;

public abstract class DoublePanelMenuCommand : ICommand
{
    private protected readonly IRenderService RenderService;
    private protected readonly IPanelBuilderService PanelBuilderService;
    
    private protected bool IsMenuRunning; 
    private protected IEnumerable<IViewModelEntity> LeftPanelEntities;
    private protected IEnumerable<IViewModelEntity> RightPanelEntries;

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
        
        var viewModel = CreateViewModel();
        var selectionService = GetSelectionService(viewModel);
        IsMenuRunning = true;
        
        GetPanels(viewModel, out var leftPanel, out var rightPanel);
        RenderService.RenderDoublePanelLayout(leftPanel, rightPanel);

        while (IsMenuRunning)
        {
            ProcessUserInput(selectionService, viewModel);
            
            GetPanels(viewModel, out leftPanel, out rightPanel);
            RenderService.RenderDoublePanelLayout(leftPanel, rightPanel);
        }
    }

    private protected abstract IEnumerable<IViewModelEntity> FetchLeftPanelData();
    
    private protected abstract IEnumerable<IViewModelEntity> FetchRightPanelData(int entityId);
    
    private protected virtual DoublePanelViewModel CreateViewModel() => 
        new(LeftPanelEntities);

    private protected virtual SelectionService GetSelectionService(
        DoublePanelViewModel viewModel) =>
        new(
            new DoublePanelSelectionStrategy(viewModel)
            );

    private protected virtual void GetPanels(
        DoublePanelViewModel viewModel, 
        out Panel leftPanel, out Panel rightPanel)
    {
        leftPanel =
            PanelBuilderService.CreatePanel(
                viewModel: viewModel.LeftPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                color: Color.Green,
                isSinglePanel: viewModel.IsSinglePanelMode,
                textDecorations: [ TextDecorations.Bold, TextDecorations.Underline ]
            );

        rightPanel = GetRightPanel(viewModel);
    }

    private protected virtual Panel GetRightPanel(DoublePanelViewModel viewModel) =>
        viewModel switch
        {
            { SelectedFilterIndex: < 0 } 
                => PanelBuilderService.CreateDummyPanel(
                "[grey]Choose a filter to see available shifts...[/]"),
            { SelectedFilterIndex: >= 0, RightPanelViewModel.PanelEntries.VisibleElements.Count: 0 } 
                => PanelBuilderService.CreateDummyPanel(
                "[grey]No shifts available for this filter...[/]"),
            _ => PanelBuilderService.CreatePanel(
                viewModel: viewModel.RightPanelViewModel,
                textAlignment: HorizontalAlignment.Left, 
                color: Color.Blue, 
                isSinglePanel: !viewModel.IsSinglePanelMode,
                textDecorations: [TextDecorations.Bold, TextDecorations.Underline])
        };

    private protected virtual void ProcessUserInput(SelectionService selectionService, DoublePanelViewModel viewModel)
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
                viewModel.UpdateRightPanelViewModel(RightPanelEntries);
                break;
            case ConsoleKey.Escape:
                IsMenuRunning = false;
                break;
        }
    }
}