using ShiftsLogger.ConsoleApp.Controllers;
using ShiftsLogger.Domain.Models.Entities;
using ShiftsLogger.View.Interfaces;

namespace ShiftsLogger.ConsoleApp.Commands.MainMenu;

public class ShiftsByLocationCommand : ShiftsCommandBase<Location>
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