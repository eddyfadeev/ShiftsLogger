using Contracts;
using Entity;
using LoggerService.Extensions;
using Microsoft.Extensions.Options;
using Services.Contracts.Clients;
using Shared.Dto.Shift;
using Shared.RequestFeatures;
using UriExtensions;

namespace Services.HttpClients;

internal sealed class ShiftClient : HttpClientBase<ShiftDto>, IShiftService
{
    private readonly Uri _defaultEndpoint;
    
    public ShiftClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> endpointOptions, ILoggerManager logger)
        : base(httpClient, endpointOptions, logger)
    {
        _defaultEndpoint = new Uri(Endpoints.Value.Shifts, UriKind.Relative);

        Logger.LogDefaultEndpoint(nameof(ShiftClient), _defaultEndpoint);
    }

    public async Task<ShiftDto?> GetShiftByIdAsync(Guid shiftId)
    {
        Logger.LogEntityId(nameof(GetShiftByIdAsync), nameof(ShiftDto), shiftId);
        var uri = _defaultEndpoint.AppendGuid(shiftId);
        
        Logger.LogApiTransactionStart(nameof(GetShiftByIdAsync), uri);
        var result = await GetByIdAsync(uri);
        
        Logger.LogApiTransactionEnd(nameof(GetShiftByIdAsync));
        return result;
    }

    public async Task<PagedList<ShiftDto>> GetAllShiftsAsync(ShiftParameters? requestParameters = null)
    {
        Logger.LogPassedObject(nameof(GetAllShiftsAsync), requestParameters);
        var uri = _defaultEndpoint.AddQueryParameters(requestParameters);
        
        Logger.LogApiTransactionStart(nameof(GetAllShiftsAsync), uri);
        var result = await GetAllAsync(uri);

        Logger.LogApiTransactionEnd(nameof(GetAllShiftsAsync));
        return result;
    }

    public async Task<ShiftDto?> CreateShiftAsync(ShiftDto shift)
    {
        Logger.LogPassedObject(nameof(CreateShiftAsync), shift);
        Logger.LogApiTransactionStart(nameof(CreateShiftAsync), _defaultEndpoint);
        var result = await CreateAsync(_defaultEndpoint, shift);

        Logger.LogApiTransactionEnd(nameof(CreateShiftAsync));
        return result;
    }

    public async Task DeleteShiftAsync(Guid shiftId)
    {
        Logger.LogEntityId(nameof(DeleteShiftAsync), nameof(ShiftDto), shiftId);
        
        var uri = _defaultEndpoint.AppendGuid(shiftId);
        Logger.LogApiTransactionStart(nameof(DeleteShiftAsync), uri);
        
        await DeleteAsync(uri);
        Logger.LogApiTransactionEnd(nameof(DeleteShiftAsync));
    }

    public async Task UpdateShiftAsync(Guid shiftId, ShiftDto shift)
    {
        Logger.LogPassedObject(nameof(UpdateShiftAsync), shift);
        Logger.LogEntityId(nameof(UpdateShiftAsync), nameof(ShiftDto), shiftId);
        
        var uri = _defaultEndpoint.AppendGuid(shiftId);
        Logger.LogApiTransactionStart(nameof(UpdateShiftAsync), uri);
        
        await UpdateAsync(uri, shift);
        Logger.LogApiTransactionEnd(nameof(UpdateShiftAsync));
    }
}