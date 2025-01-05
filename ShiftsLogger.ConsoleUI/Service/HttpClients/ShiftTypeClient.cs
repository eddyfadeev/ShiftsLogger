using Contracts;
using Entity;
using LoggerService.Extensions;
using Microsoft.Extensions.Options;
using Service.Contracts.Clients;
using Shared.Dto.ShiftType;
using Shared.RequestFeatures;
using UriExtensions;

namespace Service.HttpClients;

internal sealed class ShiftTypeClient : HttpClientBase<ShiftTypeDto>, IShiftTypeService
{
    private readonly Uri _defaultEndpoint;
    
    public ShiftTypeClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> endpointOptions, ILoggerManager logger)
        : base(httpClient, endpointOptions, logger)
    {
        _defaultEndpoint = new Uri(Endpoints.Value.ShiftTypes, UriKind.Relative);

        Logger.LogDefaultEndpoint(nameof(ShiftTypeClient), _defaultEndpoint);
    }

    public async Task<ShiftTypeDto?> GetShiftTypeByIdAsync(Guid shiftTypeId)
    {
        Logger.LogEntityId(nameof(GetShiftTypeByIdAsync), nameof(ShiftTypeDto), shiftTypeId);
        var uri = _defaultEndpoint.AppendGuid(shiftTypeId);
        
        Logger.LogApiTransactionStart(nameof(GetShiftTypeByIdAsync), uri);
        var result = await GetByIdAsync(uri);
        
        Logger.LogApiTransactionEnd(nameof(GetShiftTypeByIdAsync));
        return result;
    }

    public async Task<PagedList<ShiftTypeDto>> GetAllShiftTypesAsync(ShiftTypeParameters? requestParameters = null)
    {
        Logger.LogPassedObject(nameof(GetAllShiftTypesAsync), requestParameters);
        var uri = _defaultEndpoint.AddQueryParameters(requestParameters);
        
        Logger.LogApiTransactionStart(nameof(GetAllShiftTypesAsync), uri);
        var result = await GetAllAsync(uri);

        Logger.LogApiTransactionEnd(nameof(GetAllShiftTypesAsync));
        return result;
    }

    public async Task<ShiftTypeDto?> CreateShiftTypeAsync(ShiftTypeDto shiftType)
    {
        Logger.LogPassedObject(nameof(CreateShiftTypeAsync), shiftType);
        Logger.LogApiTransactionStart(nameof(CreateShiftTypeAsync), _defaultEndpoint);
        var result = await CreateAsync(_defaultEndpoint, shiftType);

        Logger.LogApiTransactionEnd(nameof(CreateShiftTypeAsync));
        return result;
    }

    public async Task DeleteShiftTypeAsync(Guid shiftTypeId)
    {
        Logger.LogEntityId(nameof(DeleteShiftTypeAsync), nameof(ShiftTypeDto), shiftTypeId);
        
        var uri = _defaultEndpoint.AppendGuid(shiftTypeId);
        Logger.LogApiTransactionStart(nameof(DeleteShiftTypeAsync), uri);
        
        await DeleteAsync(uri);
        Logger.LogApiTransactionEnd(nameof(DeleteShiftTypeAsync));
    }

    public async Task UpdateShiftTypeAsync(Guid shiftTypeId, ShiftTypeDto shiftType)
    {
        Logger.LogPassedObject(nameof(UpdateShiftTypeAsync), shiftType);
        Logger.LogEntityId(nameof(UpdateShiftTypeAsync), nameof(ShiftTypeDto), shiftTypeId);
        
        var uri = _defaultEndpoint.AppendGuid(shiftTypeId);
        Logger.LogApiTransactionStart(nameof(UpdateShiftTypeAsync), uri);
        
        await UpdateAsync(uri, shiftType);
        Logger.LogApiTransactionEnd(nameof(UpdateShiftTypeAsync));
    }
}