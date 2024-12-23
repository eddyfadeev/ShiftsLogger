using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Contracts;
using Entity;
using LoggerService.Extensions;
using Microsoft.Extensions.Options;
using Service.Contracts.Clients;
using Service.Extensions;
using Shared.RequestFeatures;

namespace Service.HttpClients;

public abstract class HttpClientBase<TEntity> : IHttpClientBase<TEntity>, IAsyncDisposable
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    protected readonly IOptions<ApiEndpointsOptions> Endpoints;
    protected readonly ILoggerManager Logger;
    protected readonly HttpClient Client;
    
    protected HttpClientBase(HttpClient httpClient, IOptions<ApiEndpointsOptions> endpointOptions, ILoggerManager logger)
    {
        Logger = logger;
        Endpoints = endpointOptions;
        Client = httpClient;
        Client.ConfigureHttpClient(endpointOptions);
        
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        };
        
        Logger.LogInfo($"[{nameof(HttpClientBase<TEntity>)}] Base url: {Client.BaseAddress}");
    }

    public async Task<TEntity?> GetByIdAsync(Uri endpoint)
    {
        Logger.LogApiTransactionStart(nameof(GetByIdAsync), endpoint);
        var response = await Client.GetAsync(endpoint);
        
        Logger.LogStatusCode(nameof(GetByIdAsync), response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        var result = 
            await response.Content.ReadFromJsonAsync<TEntity>(_jsonSerializerOptions) 
            ?? default;
        Logger.LogApiCallHeaders(nameof(GetByIdAsync), response);
        Logger.LogApiCallContent<TEntity>(nameof(GetByIdAsync), result);
        
        Logger.LogApiTransactionEnd(nameof(GetByIdAsync));
        
        return result;
    }

    public async Task<PagedList<TEntity>> GetAllAsync(Uri endpoint)
    {
        Logger.LogApiTransactionStart(nameof(GetAllAsync), endpoint);
        var response = await Client.GetAsync(endpoint);
        
        Logger.LogStatusCode(nameof(GetAllAsync), response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        var result = 
            await response.Content.ReadFromJsonAsync<List<TEntity>>(_jsonSerializerOptions) 
            ?? [];
        Logger.LogApiCallHeaders(nameof(GetAllAsync), response);
        Logger.LogApiCallContent(nameof(GetAllAsync), result);
        
        var metadata = ExtractPaginationMetaData(response.Headers);

        Logger.LogApiTransactionEnd(nameof(GetAllAsync));
        return new PagedList<TEntity>(metadata.TotalCount, metadata.CurrentPage, metadata.PageSize, result);
    }

    public async Task<TEntity?> CreateAsync(Uri endpoint, TEntity entity)
    {
        Logger.LogApiTransactionStart(nameof(CreateAsync), endpoint);
        Logger.LogPostData(nameof(CreateAsync), entity);
        var response = await Client.PostAsJsonAsync(endpoint, entity, _jsonSerializerOptions);
        
        Logger.LogStatusCode(nameof(CreateAsync), response.StatusCode);
        response.EnsureSuccessStatusCode();

        var result = 
            await response.Content.ReadFromJsonAsync<TEntity>(_jsonSerializerOptions)
            ?? default;
        Logger.LogApiCallHeaders(nameof(CreateAsync), response);
        Logger.LogApiCallContent<TEntity>(nameof(CreateAsync), result);

        Logger.LogApiTransactionEnd(nameof(CreateAsync));
        return result;
    }
    
    public async Task UpdateAsync(Uri endpoint, TEntity? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        
        Logger.LogApiTransactionStart(nameof(UpdateAsync), endpoint);
        Logger.LogPostData(nameof(UpdateAsync), entity);
        var response = await Client.PutAsJsonAsync(endpoint, entity, _jsonSerializerOptions);
        
        Logger.LogStatusCode(nameof(UpdateAsync), response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        Logger.LogApiCallHeaders(nameof(UpdateAsync), response);
        Logger.LogApiTransactionEnd(nameof(UpdateAsync));
    }

    public async Task DeleteAsync(Uri endpoint)
    {
        Logger.LogApiTransactionStart(nameof(DeleteAsync), endpoint);
        var response = await Client.DeleteAsync(endpoint);
        
        Logger.LogStatusCode(nameof(DeleteAsync), response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        Logger.LogApiCallHeaders(nameof(DeleteAsync), response);
        Logger.LogApiTransactionEnd(nameof(DeleteAsync));
    }

    private PaginationMetaData ExtractPaginationMetaData(HttpResponseHeaders headers)
    {
        const string paginationHeader = "X-Pagination";

        if (headers.TryGetValues(paginationHeader, out var values))
        {
            return JsonSerializer.Deserialize<PaginationMetaData>(
                values.FirstOrDefault() ?? string.Empty, _jsonSerializerOptions)
                ?? new PaginationMetaData();
        }

        return new PaginationMetaData();
    }

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