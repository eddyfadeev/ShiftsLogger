using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Extensions;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class AllShiftsMenuCommand : DoublePanelMenuCommand
{
    private readonly ShiftsController _shiftsController;

    public AllShiftsMenuCommand(IRenderService renderService, IPanelBuilderService panelBuilderService, ShiftsController shiftsController) 
        : base(renderService, panelBuilderService)
    {
        _shiftsController = shiftsController;
    }

    protected override IEnumerable<ShiftViewEntity> FetchLeftPanelData() =>
        _shiftsController.GetAllShifts().Result.Select(s => s.MapToViewEntity());

    protected override IEnumerable<ShiftViewEntity> FetchRightPanelData(int entityId) =>
        [_shiftsController.GetShiftById(entityId).Result.MapToViewEntity()];
}