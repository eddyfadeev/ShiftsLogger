using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Infrastructure.Interfaces;
using ShiftsLogger.Infrastructure.Services;

namespace ShiftsLogger.Infrastructure.Extensions;

public static class HttpClientExtensions
{
    public static void ConfigureHttpClient(this HttpClient client, Uri baseUrl, string headerTypes)
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(headerTypes));
        client.BaseAddress = baseUrl;
    }

    public static void RegisterHttpManager(this IServiceCollection services)
    {
        services.AddScoped<IHttpManager, HttpManager>();
    }
}