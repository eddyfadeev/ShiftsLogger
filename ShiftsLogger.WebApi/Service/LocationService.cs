using Contracts;
using Service.Contracts;
using Shared.Dto;
using Shared.Mapper;
using Shared.RequestFeatures;

namespace Service;

internal sealed class LocationService : ILocationService
{
    private readonly IRepositoryManager _repository;

    public LocationService(IRepositoryManager repository) =>
        _repository = repository;
    

    public async Task<(List<LocationDto> locations, PaginationMetaData metaData)> 
        GetAllLocationsAsync(LocationParameters queryParameters, bool trackChanges)
    {
        var locations = await _repository.Location.GetAllLocationsAsync(queryParameters, trackChanges);

        var dtos = locations.Select(l => l.MapToDto()).ToList();

        return (dtos, locations.PaginationMetaData);
    }
}