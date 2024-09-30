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

public abstract class ShiftsCommandBase<TEntity> : ICommand
    where TEntity : class, IReportModel
{
    private readonly IRenderService _renderService;
    private readonly IPanelBuilder _panelBuilder;
    
    private bool _isMenuRunning; 

    private protected List<TEntity> Entries;
    private protected List<Shift> ShiftsByEntity;

    protected ShiftsCommandBase(IRenderService renderService, IPanelBuilder panelBuilder)
    {
        _renderService = renderService;
        _panelBuilder = panelBuilder;
        
        Entries = [];
        ShiftsByEntity = [];
    }
    
    public virtual void Execute()
    {
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

    private protected abstract List<TEntity> PopulateEntities();
    
    private protected abstract void PopulateShifts(int entityId);
    
    private DoublePanelViewModel<TEntity, Shift> CreateViewModel() => new(Entries);

    private SelectionService GetSelectionService(DoublePanelViewModel<TEntity, Shift> viewModel) =>
        new(new DoublePanelSelectionStrategy<TEntity, Shift>(viewModel));

    private void GetPanels(DoublePanelViewModel<TEntity, Shift> viewModel, out Panel leftPanel, out Panel rightPanel)
    {
        leftPanel =
            _panelBuilder.CreatePanel(
                renderInfo: viewModel.LeftPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                color: Color.Green,
                isSinglePanel: viewModel.IsSinglePanelMode,
                textDecorations: [ TextDecorations.Bold, TextDecorations.Underline]
            );

        rightPanel = GetRightPanel(viewModel);
    }

    private Panel GetRightPanel(DoublePanelViewModel<TEntity, Shift> viewModel) =>
        viewModel switch
        {
            { SelectedFilterIndex: < 0 } 
                => _panelBuilder.CreateDummyPanel(
                "[grey]Choose a filter to see available shifts...[/]"),
            { SelectedFilterIndex: >= 0, RightPanelViewModel.PanelEntries.VisibleElements.Count: 0 } 
                => _panelBuilder.CreateDummyPanel(
                "[grey]No shifts available for this filter...[/]"),
            _ => _panelBuilder.CreatePanel(
                renderInfo: viewModel.RightPanelViewModel,
                textAlignment: HorizontalAlignment.Left, 
                color: Color.Blue, 
                isSinglePanel: !viewModel.IsSinglePanelMode,
                textDecorations: [TextDecorations.Bold, TextDecorations.Underline])
        };

    private void ProcessUserInput(SelectionService selectionService, DoublePanelViewModel<TEntity,Shift> viewModel)
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
                PopulateShifts(viewModel.SelectedFilterIndex);
                viewModel.UpdateRightPanelViewModel(ShiftsByEntity);
                break;
            case ConsoleKey.Escape:
                _isMenuRunning = false;
                break;
        }
    }
}