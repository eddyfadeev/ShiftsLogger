using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Services;
using ShiftsLogger.View.Strategies.Selection;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByLocationCommand : DoublePanelCommandBase<Location>
{
    private readonly LocationsController _locationsController;

    public ShiftsByLocationCommand(
        IRenderService renderService,
        IPanelBuilderService panelBuilderService,
        LocationsController locationsController
    ) : base(renderService, panelBuilderService)
    {
        _locationsController = locationsController;
        
        Entities = PopulateEntities();
        ShiftsByEntity = PopulateShifts();
    }

    private protected sealed override List<Location> PopulateEntities() =>
        _locationsController.GetAllLocations().Result;
    
    private protected sealed override Dictionary<Location, List<Shift>> PopulateShifts()
    {
        var result = new Dictionary<Location, List<Shift>>();
        
        foreach (var location in Entities)
        {
            result.Add(
                location, _locationsController.GetShiftsByLocationId(location.Id).Result);
        }

        return result;
    }
}

public abstract class DoublePanelCommandBase<TEntity> : ICommand
    where TEntity : class, IReportModel
{
    private protected readonly IRenderService _renderService;
    private protected readonly IPanelBuilderService _panelBuilderService;

    private protected List<TEntity> Entities;
    private protected Dictionary<TEntity, List<Shift>> ShiftsByEntity;
    private protected bool IsMenuRunning; 

    protected DoublePanelCommandBase(IRenderService renderService, IPanelBuilderService panelBuilderService)
    {
        _renderService = renderService;
        _panelBuilderService = panelBuilderService;

        Entities = new List<TEntity>();
        ShiftsByEntity = new Dictionary<TEntity, List<Shift>>();
    }
    public virtual void Execute()
    {
        var viewModel = CreateViewModel();
        var selectionService = GetSelectionService(viewModel);
        IsMenuRunning = true;

        while (IsMenuRunning)
        {
            GetPanels(viewModel, out var leftPanel, out var rightPanel);
            
            _renderService.RenderDoublePanelLayout(leftPanel, rightPanel);

            ProcessUserInput(selectionService);
        }
    }

    private protected DoublePanelViewModel<TEntity, Shift> CreateViewModel() => new(Entities, ShiftsByEntity);

    private protected SelectionService GetSelectionService(DoublePanelViewModel<TEntity, Shift> viewModel) =>
        new(new DoublePanelSelectionStrategy<TEntity, Shift>(viewModel));

    private protected void GetPanels(DoublePanelViewModel<TEntity, Shift> viewModel, out Panel leftPanel, out Panel rightPanel)
    {
        var panels =
            _panelBuilderService.PrepareRenderablePanels(
                renderInfo: viewModel,
                leftPanelTextAlignment: HorizontalAlignment.Left,
                rightPanelTextAlignment: HorizontalAlignment.Left
            );
        
        panels.Deconstruct(out leftPanel, out rightPanel);
    } 

    private protected abstract List<TEntity> PopulateEntities();
    private protected abstract Dictionary<TEntity, List<Shift>> PopulateShifts();

    private protected void ProcessUserInput(SelectionService selectionService)
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
                break;
            case ConsoleKey.Escape:
                IsMenuRunning = false;
                break;
        }
    }
} 