using Shared.Dto;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ILocationService
{
    Task<(List<LocationDto> locations, PaginationMetaData metaData)> 
        GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges);
}