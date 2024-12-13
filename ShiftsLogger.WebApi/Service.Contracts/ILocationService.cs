using Shared.Dto.Location;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ILocationService
{
    Task<(List<LocationDto> locations, PaginationMetaData metaData)> 
        GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges);
    Task<LocationDto> GetLocationById(Guid locationId, bool trackChanges);
    Task CreateLocation(LocationForCreationDto location);
    Task DeleteLocation(Guid locationId, bool trackChanges);
    Task UpdateLocation(Guid locationId, LocationForUpdateDto updateDto, bool trackChanges);
    
}