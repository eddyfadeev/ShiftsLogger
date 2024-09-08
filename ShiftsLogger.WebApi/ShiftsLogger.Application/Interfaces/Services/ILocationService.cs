using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface ILocationService
{
    List<Location> GetAllLocations();
    int AddLocation(Location location);
    Location? GetLocation(int locationId);
    int UpdateLocation(Location location);
    int RemoveLocation(Location location);
}