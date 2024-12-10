using Entities.Models;

namespace Contracts.Repository;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocationsAsync(bool trackChanges);
    Task<Location?> GetLocationByIdAsync(Guid locationId, bool trackChanges);
    void CreateLocation(Location location);
    void DeleteLocation(Location location);
    void UpdateLocation(Location location);
    bool LocationExists(Guid locationId);
}