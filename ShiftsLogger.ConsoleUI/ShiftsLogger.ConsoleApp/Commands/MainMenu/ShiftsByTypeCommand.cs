using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByTypeCommand : ShiftsCommandBase<ShiftType, Shift>
{
    private readonly ShiftTypesController _shiftTypesController;
    
    public ShiftsByTypeCommand(IRenderService renderService, IPanelBuilderService panelBuilderService, ShiftTypesController shiftTypesController) 
        : base(renderService, panelBuilderService)
    {
        _shiftTypesController = shiftTypesController;

        LeftPanelEntities = FetchLeftPanelData();
    }

    private protected sealed override List<ShiftType> FetchLeftPanelData() =>
        _shiftTypesController.GetAllShiftTypes().Result;

    private protected sealed override void FetchRightPanelData(int entityId) =>
        RightPanelEntries = _shiftTypesController.GetShiftsByShiftTypeId(entityId).Result;
}