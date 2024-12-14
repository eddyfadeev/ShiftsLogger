using Shared.Dto.Location;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ILocationService
{
    Task<(List<LocationDto> locations, PaginationMetaData metaData)> 
        GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges);
    Task<LocationDto> GetLocationByIdAsync(Guid locationId, bool trackChanges);
    Task<LocationDto> CreateLocationAsync(LocationForCreationDto location);
    Task DeleteLocationAsync(Guid locationId, bool trackChanges);
    Task UpdateLocationAsync(Guid locationId, LocationForUpdateDto updateDto, bool trackChanges);
}