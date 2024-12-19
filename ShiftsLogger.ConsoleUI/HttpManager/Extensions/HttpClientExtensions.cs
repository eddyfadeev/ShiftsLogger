using System.Collections.Immutable;
using System.Net.Http.Headers;
using Entity;
using Microsoft.Extensions.Options;

namespace HttpManager.Extensions;

public static class HttpClientExtensions
{
    public static void ConfigureHttpClient(this HttpClient client, IOptions<ApiEndpointsOptions> endpointOptions)
    {
        ImmutableList<string> headers = ["application/json", "text/xml"];
        
        client.BaseAddress = new Uri(endpointOptions.Value.BaseUrl);
        headers.ForEach(h =>
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(h)));
    }
}