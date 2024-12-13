using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Repository;

public interface ILocationRepository
{
    Task<PagedList<Location>> GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges);
    Task<Location> GetLocationByIdAsync(Guid locationId, bool trackChanges);
    void CreateLocation(Location location);
    void DeleteLocation(Location location);
    void UpdateLocation(Location location);
    bool LocationExists(Guid locationId);
}