using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationsRepository _locationsRepository;

    public LocationService(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }


    public List<Location> GetAllLocations() => 
        _locationsRepository.GetAllLocations();

    public int AddLocation(Location location) =>
        _locationsRepository.Add(location);

    public Location? GetLocation(int locationId) =>
        _locationsRepository.Get(locationId);

    public int UpdateLocation(Location location) =>
        _locationsRepository.Update(location);

    public int RemoveLocation(Location location) =>
        _locationsRepository.Remove(location);
}