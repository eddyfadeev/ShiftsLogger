using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Interfaces;

namespace ShiftsLogger.Infrastructure.Handlers;

public class RequestHandler : IRequestHandler
{
    private readonly IHttpManager _httpManager;

    public RequestHandler(IHttpManager httpManager)
    {
        _httpManager = httpManager;
    }

    public async Task<string> GetAsync(Uri url)
    {
        using var client = _httpManager.GetHttpClient();
        
        var response = await client.GetAsync(url);
        
        _httpManager.EnsureSuccessStatusCode(response, url);
        
        var result = await response.Content.ReadAsStringAsync();
        
        return result;
    }

    public async Task<HttpResponseMessage> PostAsync(Uri url, HttpContent content)
    {
        using var client = _httpManager.GetHttpClient();
        var response = await client.PostAsync(url, content);

        _httpManager.EnsureSuccessStatusCode(response, url);

        return response;
    }

    public async Task<HttpResponseMessage?> DeleteAsync(Uri url)
    {
        using var client = _httpManager.GetHttpClient();
        var response = await client.DeleteAsync(url);
        
        _httpManager.EnsureSuccessStatusCode(response, url);

        return response;
    }
}