using Contracts;
using Entity;
using LoggerService.Extensions;
using Microsoft.Extensions.Options;
using Service.Contracts.Clients;
using Shared.Dto.Location;
using Shared.RequestFeatures;
using UriExtensions;

namespace Service.Clients;

internal sealed class LocationClient : HttpClientBase<LocationDto>, ILocationService
{
    private readonly Uri _defaultEndpoint;
    
    public LocationClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> endpointOptions, ILoggerManager logger)
        : base(httpClient, endpointOptions, logger)
    {
        _defaultEndpoint = new Uri(Endpoints.Value.Locations, UriKind.Relative);

        Logger.LogDefaultEndpoint(nameof(LocationClient), _defaultEndpoint);
    }

    public async Task<LocationDto?> GetLocationByIdAsync(Guid locationId)
    {
        Logger.LogEntityId(nameof(GetLocationByIdAsync), nameof(LocationDto), locationId);
        var uri = _defaultEndpoint.AppendGuid(locationId);
        
        Logger.LogApiTransactionStart(nameof(GetLocationByIdAsync), uri);
        var result = await GetByIdAsync(uri);
        
        Logger.LogApiTransactionEnd(nameof(GetLocationByIdAsync));
        return result;
    }

    public async Task<PagedList<LocationDto>> GetAllLocationsAsync(LocationParameters? requestParameters = null)
    {
        Logger.LogPassedObject(nameof(GetAllLocationsAsync), requestParameters);
        var uri = _defaultEndpoint.AddQueryParameters(requestParameters);
        
        Logger.LogApiTransactionStart(nameof(GetAllLocationsAsync), uri);
        var result = await GetAllAsync(uri);

        Logger.LogApiTransactionEnd(nameof(GetAllLocationsAsync));
        return result;
    }

    public async Task<LocationDto?> CreateLocationAsync(LocationDto location)
    {
        Logger.LogPassedObject(nameof(CreateLocationAsync), location);
        Logger.LogApiTransactionStart(nameof(CreateLocationAsync), _defaultEndpoint);
        var result = await CreateAsync(_defaultEndpoint, location);

        Logger.LogApiTransactionEnd(nameof(CreateLocationAsync));
        return result;
    }

    public async Task DeleteLocationAsync(Guid locationId)
    {
        Logger.LogEntityId(nameof(DeleteLocationAsync), nameof(LocationDto), locationId);
        
        var uri = _defaultEndpoint.AppendGuid(locationId);
        Logger.LogApiTransactionStart(nameof(DeleteLocationAsync), uri);
        
        await DeleteAsync(uri);
        Logger.LogApiTransactionEnd(nameof(DeleteLocationAsync));
    }

    public async Task UpdateLocationAsync(Guid locationId, LocationDto location)
    {
        Logger.LogPassedObject(nameof(UpdateLocationAsync), location);
        Logger.LogEntityId(nameof(UpdateLocationAsync), nameof(LocationDto), locationId);
        
        var uri = _defaultEndpoint.AppendGuid(locationId);
        Logger.LogApiTransactionStart(nameof(UpdateLocationAsync), uri);
        
        await UpdateAsync(uri, location);
        Logger.LogApiTransactionEnd(nameof(UpdateLocationAsync));
    }
}