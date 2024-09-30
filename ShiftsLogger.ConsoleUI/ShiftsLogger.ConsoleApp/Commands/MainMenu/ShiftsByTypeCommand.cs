using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByTypeCommand : ShiftsCommandBase<ShiftType>
{
    private readonly ShiftTypesController _shiftTypesController;
    
    public ShiftsByTypeCommand(IRenderService renderService, IPanelBuilder panelBuilder, ShiftTypesController shiftTypesController) 
        : base(renderService, panelBuilder)
    {
        _shiftTypesController = shiftTypesController;

        Entries = PopulateEntities();
    }

    private protected sealed override List<ShiftType> PopulateEntities() =>
        _shiftTypesController.GetAllShiftTypes().Result;

    private protected sealed override void PopulateShifts(int entityId) =>
        ShiftsByEntity = _shiftTypesController.GetShiftsByShiftTypeId(entityId).Result;
}