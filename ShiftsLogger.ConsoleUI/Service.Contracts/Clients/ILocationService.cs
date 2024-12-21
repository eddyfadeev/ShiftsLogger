using Shared.Dto.Location;
using Shared.RequestFeatures;

namespace Service.Contracts.Clients;

public interface ILocationService
{
    Task<LocationDto?> GetLocationByIdAsync(Guid locationId);
    Task<PagedList<LocationDto>> GetAllLocationsAsync(LocationParameters? requestParameters = null);
    Task<LocationDto?> CreateLocationAsync(LocationDto location);
    Task DeleteLocationAsync(Guid locationId);
    Task UpdateLocationAsync(Guid locationId, LocationDto location);
}