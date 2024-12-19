using System.Net.Http.Json;
using System.Text.Json;
using Contracts;
using Entity;
using HttpManager.Extensions;
using Microsoft.Extensions.Options;
using Shared.Dto.User;
using Shared.RequestFeatures;

namespace HttpManager;

public abstract class HttpClientBase<TEntity> : IHttpClientBase<TEntity>, IAsyncDisposable
{
    protected readonly IOptions<ApiEndpointsOptions> Endpoints;
    protected readonly HttpClient Client;

    protected HttpClientBase(IHttpClientFactory factory, IOptions<ApiEndpointsOptions> endpointOptions)
    {
        Endpoints = endpointOptions;
        Client = factory.CreateClient();
        Client.ConfigureHttpClient(endpointOptions);
    }

    public async Task<TEntity?> GetByIdAsync(Uri endpoint) =>
        await Client.GetFromJsonAsync<TEntity>(endpoint);

    public async Task<PagedList<TEntity>?> GetAllAsync(Uri endpoint)
    {
        var result = await Client.GetAsync(endpoint);
        var list = await result.Content.ReadFromJsonAsync<List<TEntity>>();
        result.Headers.TryGetValues("X-Pagination", out var values);
        var metadata = JsonSerializer.Deserialize<PaginationMetaData>(values?.ElementAt(0) ?? "");
        
        return new PagedList<TEntity>(metadata.TotalCount, metadata.CurrentPage, metadata.PageSize, list);
    }

    public async Task<TEntity?> CreateAsync(Uri endpoint, TEntity entity)
    {
        var result = await Client.PostAsJsonAsync(endpoint, entity);

        return await result.Content.ReadFromJsonAsync<TEntity>();
    }
    
    public async Task UpdateAsync(Uri endpoint, TEntity entity) =>
        await Client.PutAsJsonAsync(endpoint, entity);
    
    public async Task DeleteAsync(Uri uri) =>
        await Client.DeleteAsync(uri);

    #region Dispose

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (Client is IAsyncDisposable clientAsyncDisposable)
        {
            await clientAsyncDisposable.DisposeAsync();
        }
        else
        {
            Client.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    #endregion
}

public class UserClient : HttpClientBase<UserDto>
{
    public UserClient(IHttpClientFactory factory, IOptions<ApiEndpointsOptions> endpointOptions) : base(factory, endpointOptions)
    {
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId, UserParameters? requestParameters = null)
    {
        var uri = new Uri($"{Endpoints.Value.Users}/{userId}", UriKind.Relative);

        var result = await GetByIdAsync(uri);
        
        return result;
    }

    public async Task<List<UserDto>> GetAllUsersAsync(UserParameters? requestParameters = null)
    {
        var uri = new Uri(Endpoints.Value.Users, UriKind.Relative);

        var result = await GetAllAsync(uri);

        return result.Select(u => u).ToList();
    }

    public async Task<UserDto> CreateUser(UserDto user)
    {
        var uri = new Uri(Endpoints.Value.Users, UriKind.Relative);

        var result = await CreateAsync(uri, user);

        return result;
    }

    public async Task DeleteUser(Guid userId)
    {
        var uri = new Uri($"{Endpoints.Value.Users}/{userId}", UriKind.Relative);

        await DeleteAsync(uri);
    }

    public async Task UpdateAsync(UserDto user)
    {
        var uri = new Uri($"{Endpoints.Value.Users}/{user.Id}", UriKind.Relative);

        await UpdateAsync(uri, user);
    }
}