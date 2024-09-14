﻿using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Interfaces;
using ShiftsLogger.Infrastructure.Configurations;
using ShiftsLogger.Infrastructure.Extensions;

namespace ShiftsLogger.Infrastructure.Services;

public class HttpManager : IHttpManager
{
    private const string DefaultHeader = "application/json";
    private readonly Uri _baseUrl;

    public HttpManager(IOptions<ApiConfig> apiConfig)
    {
        _baseUrl = new Uri(apiConfig.Value.BaseUrl
                           ?? throw new InvalidOperationException("Base URL is null."));
    }

    public async Task<string> GetAsync(Uri url)
    {
        using var client = GetHttpClient();
        
        var response = await client.GetAsync(url);
        
        EnsureSuccessStatusCode(response, url);
        
        var result = await response.Content.ReadAsStringAsync();
        
        return result;
    }

    public async Task PostAsync<TEntity>(Uri url, TEntity data)
    {
        using var client = GetHttpClient();
        var response = await client.PostAsJsonAsync(url, data);

        EnsureSuccessStatusCode(response, url);
    }

    public async Task<HttpResponseMessage?> DeleteAsync(Uri url, int id)
    {
        using var client = GetHttpClient();
        
        var response = await client.DeleteAsync($"{url}/{id}");
        EnsureSuccessStatusCode(response, url);
        
        return response;
    }

    private HttpClient GetHttpClient()
    {
        HttpClient client = new();
        
        client.ConfigureHttpClient(_baseUrl, DefaultHeader);
        return client;
    }

    private static void EnsureSuccessStatusCode(HttpResponseMessage response, Uri url)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to post data to {url}. Status code: {response.StatusCode}");
        }
    }
}