using Contracts;
using Entity;
using LoggerService.Extensions;
using Microsoft.Extensions.Options;
using Services.Contracts.Clients;
using Shared.Dto.User;
using Shared.RequestFeatures;
using UriExtensions;

namespace Services.HttpClients;

internal sealed class UserClient : HttpClientBase<UserDto>, IUserService
{
    private readonly Uri _defaultEndpoint;
    
    public UserClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> endpointOptions, ILoggerManager logger)
        : base(httpClient, endpointOptions, logger)
    {
        _defaultEndpoint = new Uri(Endpoints.Value.Users, UriKind.Relative);

        Logger.LogDefaultEndpoint(nameof(UserClient), _defaultEndpoint);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        Logger.LogEntityId(nameof(GetUserByIdAsync), nameof(UserDto), userId);
        var uri = _defaultEndpoint.AppendGuid(userId);
        
        Logger.LogApiTransactionStart(nameof(GetUserByIdAsync), uri);
        var result = await GetByIdAsync(uri);
        
        Logger.LogApiTransactionEnd(nameof(GetUserByIdAsync));
        return result;
    }

    public async Task<PagedList<UserDto>> GetAllUsersAsync(UserParameters? requestParameters = null)
    {
        Logger.LogPassedObject(nameof(GetAllUsersAsync), requestParameters);
        var uri = _defaultEndpoint.AddQueryParameters(requestParameters);
        
        Logger.LogApiTransactionStart(nameof(GetAllUsersAsync), uri);
        var result = await GetAllAsync(uri);

        Logger.LogApiTransactionEnd(nameof(GetAllUsersAsync));
        return result;
    }

    public async Task<UserDto?> CreateUserAsync(UserDto user)
    {
        Logger.LogPassedObject(nameof(CreateUserAsync), user);
        Logger.LogApiTransactionStart(nameof(CreateUserAsync), _defaultEndpoint);
        var result = await CreateAsync(_defaultEndpoint, user);

        Logger.LogApiTransactionEnd(nameof(CreateUserAsync));
        return result;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        Logger.LogEntityId(nameof(DeleteUserAsync), nameof(UserDto), userId);
        
        var uri = _defaultEndpoint.AppendGuid(userId);
        Logger.LogApiTransactionStart(nameof(DeleteUserAsync), uri);
        
        await DeleteAsync(uri);
        Logger.LogApiTransactionEnd(nameof(DeleteUserAsync));
    }

    public async Task UpdateUserAsync(Guid userId, UserDto user)
    {
        Logger.LogPassedObject(nameof(UpdateUserAsync), user);
        Logger.LogEntityId(nameof(UpdateUserAsync), nameof(UserDto), userId);
        
        var uri = _defaultEndpoint.AppendGuid(userId);
        Logger.LogApiTransactionStart(nameof(UpdateUserAsync), uri);
        
        await UpdateAsync(uri, user);
        Logger.LogApiTransactionEnd(nameof(UpdateUserAsync));
    }
}