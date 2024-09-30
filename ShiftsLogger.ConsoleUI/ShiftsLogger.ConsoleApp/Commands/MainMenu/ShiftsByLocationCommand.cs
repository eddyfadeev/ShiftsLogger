using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByLocationCommand : ShiftsCommandBase<Location, Shift>
{
    private readonly LocationsController _locationsController;

    public ShiftsByLocationCommand(
        IRenderService renderService,
        IPanelBuilderService panelBuilderService,
        LocationsController locationsController
    ) : base(renderService, panelBuilderService)
    {
        _locationsController = locationsController;
        
        LeftPanelEntities = FetchLeftPanelData();
    }

    private protected sealed override List<Location> FetchLeftPanelData() =>
        _locationsController.GetAllLocations().Result;
    
    private protected sealed override void FetchRightPanelData(int entityId) =>
        RightPanelEntries = _locationsController.GetShiftsByLocationId(entityId).Result;
}