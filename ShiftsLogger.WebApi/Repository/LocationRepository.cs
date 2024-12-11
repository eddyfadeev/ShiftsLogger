using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    public LocationRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<IEnumerable<Location>> GetAllLocationsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(l => l.Name)
            .ToListAsync();

    public async Task<Location?> GetLocationByIdAsync(Guid locationId, bool trackChanges) =>
        await FindByCondition(l => l.Id.Equals(locationId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateLocation(Location location) =>
        Create(location);

    public void DeleteLocation(Location location) =>
        Delete(location);

    public void UpdateLocation(Location location) =>
        Update(location);

    public bool LocationExists(Guid locationId) =>
        Exists(l => l.Id.Equals(locationId));
}