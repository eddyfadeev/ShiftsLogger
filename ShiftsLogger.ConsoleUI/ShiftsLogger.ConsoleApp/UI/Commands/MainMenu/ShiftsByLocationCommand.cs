using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.ConsoleApp.UI.Extensions;
using ShiftsLogger.ConsoleApp.UI.Models;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.UI.Commands.MainMenu;

public sealed class ShiftsByLocationCommand : DoublePanelMenuCommand
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

    protected override IEnumerable<LocationViewEntity> FetchLeftPanelData() =>
        _locationsController.GetAllLocations().Result.Select(l => l.MapToViewEntity());

    protected override IEnumerable<ShiftViewEntity> FetchRightPanelData(int entityId) =>
        _locationsController.GetShiftsByLocationId(entityId).Result.Select(s => s.MapToViewEntity());
}