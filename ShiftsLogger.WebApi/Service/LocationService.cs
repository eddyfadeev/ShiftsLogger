using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.Dto.Location;
using Shared.Mappers;
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

    public async Task<LocationDto> GetLocationByIdAsync(Guid locationId, bool trackChanges)
    {
        var location = await TryGetLocationEntity(locationId, trackChanges);

        return location.MapToDto();
    }

    public async Task<LocationDto> CreateLocationAsync(LocationForCreationDto location)
    {
        var entity = location.MapToEntity();
        
        _repository.Location.CreateLocation(entity);
        await _repository.SaveAsync();

        return entity.MapToDto();
    }

    public async Task DeleteLocationAsync(Guid locationId, bool trackChanges)
    {
        var entity = await TryGetLocationEntity(locationId, trackChanges);
        
        _repository.Location.DeleteLocation(entity);
        await _repository.SaveAsync();
    }

    public async Task UpdateLocationAsync(Guid locationId, LocationForUpdateDto updateDto, bool trackChanges)
    {
        var location = await TryGetLocationEntity(locationId, trackChanges);

        location.UpdateEntity(updateDto);
        await _repository.SaveAsync();
    }

    private async Task<Location> TryGetLocationEntity(Guid locationId, bool trackChanges) =>
        (_repository.Location.LocationExists(locationId)
            ? await _repository.Location.GetLocationByIdAsync(locationId, trackChanges)
            : throw new LocationNotFoundException(locationId))!;
}