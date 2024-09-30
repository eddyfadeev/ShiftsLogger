using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByLocationCommand : ShiftsCommandBase<Location>
{
    private readonly LocationsController _locationsController;

    public ShiftsByLocationCommand(
        IRenderService renderService,
        IPanelBuilder panelBuilder,
        LocationsController locationsController
    ) : base(renderService, panelBuilder)
    {
        _locationsController = locationsController;
        
        Entries = PopulateEntities();
    }

    private protected sealed override List<Location> PopulateEntities() =>
        _locationsController.GetAllLocations().Result;
    
    private protected sealed override void PopulateShifts(int entityId) =>
        ShiftsByEntity = _locationsController.GetShiftsByLocationId(entityId).Result;
}