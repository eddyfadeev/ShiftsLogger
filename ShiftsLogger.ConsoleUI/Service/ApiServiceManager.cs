using Contracts;
using Entity;
using Microsoft.Extensions.Options;
using Service.Clients;
using Service.Contracts;
using Service.Contracts.Clients;

namespace Service;

public class ApiServiceManager : IApiServiceManager
{
    private readonly Lazy<IUserService> _userClient;
    private readonly Lazy<IShiftTypeService> _shiftTypeClient;
    private readonly Lazy<ILocationService> _locationClient;
    private readonly Lazy<IShiftService> _shiftClient;

    public ApiServiceManager(IHttpClientFactory factory, IOptions<ApiEndpointsOptions> options, ILoggerManager logger)
    {
        var httpClient = factory.CreateClient();
        
        _userClient = new Lazy<IUserService>(() => 
            new UserClient(httpClient, options, logger));

        _shiftTypeClient = new Lazy<IShiftTypeService>(() =>
            new ShiftTypeClient(httpClient, options, logger));

        _locationClient = new Lazy<ILocationService>(() =>
            new LocationClient(httpClient, options, logger));

        _shiftClient = new Lazy<IShiftService>(() =>
            new ShiftClient(httpClient, options, logger));
    }

    public IUserService User =>
        _userClient.Value;

    public IShiftTypeService ShiftType =>
        _shiftTypeClient.Value;

    public ILocationService Location =>
        _locationClient.Value;

    public IShiftService Shift =>
        _shiftClient.Value;
}