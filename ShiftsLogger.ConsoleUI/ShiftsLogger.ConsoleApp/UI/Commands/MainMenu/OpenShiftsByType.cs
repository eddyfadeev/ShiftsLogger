using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Extensions;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class OpenShiftsByType : DoublePanelMenuCommand
{
    private readonly ShiftTypesController _shiftTypesController;
    
    public OpenShiftsByType(IRenderService renderService, IPanelBuilderService panelBuilderService, ShiftTypesController shiftTypesController) 
        : base(renderService, panelBuilderService)
    {
        _shiftTypesController = shiftTypesController;

        LeftPanelEntities = FetchLeftPanelData();
    }

    protected override IEnumerable<ShiftTypeViewEntity> FetchLeftPanelData() =>
        _shiftTypesController.GetAllShiftTypes().Result.Select(st => st.MapToViewEntity());

    protected override IEnumerable<ShiftViewEntity> FetchRightPanelData(int entityId) =>
        _shiftTypesController.GetShiftsByShiftTypeId(entityId).Result.Select(s => s.MapToViewEntity());
}