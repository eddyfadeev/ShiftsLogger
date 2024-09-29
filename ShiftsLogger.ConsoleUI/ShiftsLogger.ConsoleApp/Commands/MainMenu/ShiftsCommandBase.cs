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
    private protected readonly IRenderService RenderService;
    private protected readonly IPanelBuilder PanelBuilder;

    private protected List<TEntity> Entities;
    private protected List<Shift> ShiftsByEntity;
    private protected bool IsMenuRunning; 

    protected ShiftsCommandBase(IRenderService renderService, IPanelBuilder panelBuilder)
    {
        RenderService = renderService;
        PanelBuilder = panelBuilder;
        
        Entities = new List<TEntity>();
        ShiftsByEntity = new List<Shift>();
    }
    
    public virtual void Execute()
    {
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

    private protected abstract List<TEntity> PopulateEntities();
    
    private protected abstract void PopulateShifts(int entityId);
    
    private protected DoublePanelViewModel<TEntity, Shift> CreateViewModel() => new(Entities);

    private protected SelectionService GetSelectionService(DoublePanelViewModel<TEntity, Shift> viewModel) =>
        new(new DoublePanelSelectionStrategy<TEntity, Shift>(viewModel));

    private protected void GetPanels(DoublePanelViewModel<TEntity, Shift> viewModel, out Panel leftPanel, out Panel rightPanel)
    {
        leftPanel =
            PanelBuilder.CreatePanel(
                renderInfo: viewModel.LeftPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                color: Color.Green,
                isSinglePanel: viewModel.IsSinglePanelMode,
                textDecorations: [ TextDecorations.Bold, TextDecorations.Underline]
            );

        if (viewModel.SelectedFilterIndex < 0)
        {
            rightPanel = PanelBuilder.CreateDummyPanel("[grey]Choose a filter to see available shifts...[/]");
            return;
        }

        if (viewModel.SelectedFilterIndex >= 0 && viewModel.RightPanelViewModel.PanelEntries.Count == 0)
        {
            rightPanel = PanelBuilder.CreateDummyPanel("[grey]No shifts available for this filter...[/]");
            return;
        }
        
        rightPanel = 
            PanelBuilder.CreatePanel(
                renderInfo: viewModel.RightPanelViewModel, 
                textAlignment: HorizontalAlignment.Left,
                color: Color.Blue,
                isSinglePanel: !viewModel.IsSinglePanelMode,
                textDecorations: [ TextDecorations.Bold, TextDecorations.Underline]
            );
    } 

    private protected void ProcessUserInput(SelectionService selectionService, DoublePanelViewModel<TEntity,Shift> viewModel)
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
                IsMenuRunning = false;
                break;
        }
    }
}