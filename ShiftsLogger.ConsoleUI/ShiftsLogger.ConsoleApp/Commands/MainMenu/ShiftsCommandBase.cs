using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public abstract class ShiftsCommandBase<TLeftPanelEntities, TRightPanelEntities> : ICommand
    where TLeftPanelEntities : class
    where TRightPanelEntities: class
{
    private readonly IRenderService _renderService;
    private readonly IPanelBuilderService _panelBuilderService;
    
    private protected bool _isMenuRunning; 
    private protected IEnumerable<TLeftPanelEntities> LeftPanelEntities;
    private protected IEnumerable<TRightPanelEntities> RightPanelEntries;

    protected ShiftsCommandBase(IRenderService renderService, IPanelBuilderService panelBuilderService)
    {
        _renderService = renderService;
        _panelBuilderService = panelBuilderService;
        
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
        _isMenuRunning = true;
        
        GetPanels(viewModel, out var leftPanel, out var rightPanel);
        _renderService.RenderDoublePanelLayout(leftPanel, rightPanel);

        while (_isMenuRunning)
        {
            ProcessUserInput(selectionService, viewModel);
            
            GetPanels(viewModel, out leftPanel, out rightPanel);
            _renderService.RenderDoublePanelLayout(leftPanel, rightPanel);
        }
    }

    private protected abstract IEnumerable<TLeftPanelEntities> FetchLeftPanelData();
    
    private protected abstract void FetchRightPanelData(int entityId);
    
    private protected virtual DoublePanelViewModel<TLeftPanelEntities, TRightPanelEntities> CreateViewModel() => new(LeftPanelEntities);

    private protected virtual SelectionService GetSelectionService(DoublePanelViewModel<TLeftPanelEntities, TRightPanelEntities> viewModel) =>
        new(new DoublePanelSelectionStrategy<TLeftPanelEntities, TRightPanelEntities>(viewModel));

    private protected virtual void GetPanels(DoublePanelViewModel<TLeftPanelEntities, TRightPanelEntities> viewModel, out Panel leftPanel, out Panel rightPanel)
    {
        leftPanel =
            _panelBuilderService.CreatePanel(
                renderInfo: viewModel.LeftPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                color: Color.Green,
                isSinglePanel: viewModel.IsSinglePanelMode,
                textDecorations: [ TextDecorations.Bold, TextDecorations.Underline ]
            );

        rightPanel = GetRightPanel(viewModel);
    }

    private Panel GetRightPanel(DoublePanelViewModel<TLeftPanelEntities, TRightPanelEntities> viewModel) =>
        viewModel switch
        {
            { SelectedFilterIndex: < 0 } 
                => _panelBuilderService.CreateDummyPanel(
                "[grey]Choose a filter to see available shifts...[/]"),
            { SelectedFilterIndex: >= 0, RightPanelViewModel.PanelEntries.VisibleElements.Count: 0 } 
                => _panelBuilderService.CreateDummyPanel(
                "[grey]No shifts available for this filter...[/]"),
            _ => _panelBuilderService.CreatePanel(
                renderInfo: viewModel.RightPanelViewModel,
                textAlignment: HorizontalAlignment.Left, 
                color: Color.Blue, 
                isSinglePanel: !viewModel.IsSinglePanelMode,
                textDecorations: [TextDecorations.Bold, TextDecorations.Underline])
        };

    private void ProcessUserInput(SelectionService selectionService, DoublePanelViewModel<TLeftPanelEntities, TRightPanelEntities> viewModel)
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
                FetchRightPanelData(viewModel.SelectedFilterIndex);
                viewModel.UpdateRightPanelViewModel(RightPanelEntries);
                break;
            case ConsoleKey.Escape:
                _isMenuRunning = false;
                break;
        }
    }
}