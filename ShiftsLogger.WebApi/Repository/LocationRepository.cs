using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    public LocationRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<PagedList<Location>> GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges)
    {
        var locations = await FindAll(trackChanges)
            .Search(queryParameters)
            .Sort(queryParameters)
            .Page(queryParameters)
            .ToListAsync();

        var count = await FindAll(trackChanges)
            .Search(queryParameters)
            .CountAsync();

        return new PagedList<Location>
        (
            count,
            queryParameters.PageNumber,
            queryParameters.PageSize,
            locations
        );
    }

    public async Task<Location> GetLocationByIdAsync(Guid locationId, bool trackChanges) =>
        await FindByCondition(l => l.Id.Equals(locationId), trackChanges)
            .SingleAsync();

    public void CreateLocation(Location location) =>
        Create(location);

    public void DeleteLocation(Location location) =>
        Delete(location);
    
    public bool LocationExists(Guid locationId) =>
        Exists(l => l.Id.Equals(locationId));
}