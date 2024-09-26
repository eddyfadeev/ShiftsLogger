using Microsoft.Extensions.Options;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
using ShiftsLogger.Infrastructure.Configurations;

namespace ShiftsLogger.Infrastructure.Mappers;

public class ApiEndpointMapper : IApiEndpointMapper
{
    private readonly ApiConfig _apiConfig;
    private readonly Dictionary<Type, Dictionary<string, string>> _endpointsMap;

    public ApiEndpointMapper(IOptions<ApiConfig> apiConfig)
    {
        _apiConfig = apiConfig.Value ??
                     throw new ArgumentNullException(nameof(apiConfig), "Api configuration is null.");
        
        _endpointsMap = GetEndpointsMap();
    }
    public Uri GetRelativeUrl<TApi>(TApi endpoint, int? id = null) where TApi : Enum
    {
        if(!_endpointsMap.TryGetValue(endpoint.GetType(), out var endpointMap))
        {
            throw new InvalidOperationException($"No endpoint map found for type {endpoint.GetType().Name}.");
        }
        
        string apiEndpoint = GetApiEndpointFromMap(endpointMap, endpoint.ToString());

        if (id is null)
        {
            return new Uri(apiEndpoint, UriKind.Relative);
        }
        
        apiEndpoint = ProcessIdSpecificRequest(apiEndpoint, id.Value);
        return new Uri(apiEndpoint, UriKind.Relative);
    }

    private static string ProcessIdSpecificRequest(string apiEndpoint, int id)
    {
        const string idKey = "id";
        var array = apiEndpoint.Split('/');
        int index = Array.FindIndex(
            array, s => 
                s.Contains(idKey, StringComparison.InvariantCultureIgnoreCase)
        );

        if (index != -1)
        {
            array[index] = id.ToString();
        }

        return string.Join("/", array);
    }

    private Dictionary<Type, Dictionary<string, string>> GetEndpointsMap() =>
        new()
        {
            { typeof(ApiEndpoints.Shifts), _apiConfig.Shifts },
            { typeof(ApiEndpoints.Users), _apiConfig.Users },
            { typeof(ApiEndpoints.Locations), _apiConfig.Locations },
            { typeof(ApiEndpoints.ShiftTypes), _apiConfig.ShiftTypes },
        };

    private static string GetApiEndpointFromMap(Dictionary<string, string> map, string key)
    {
        if (map.TryGetValue(key, out string endpoint))
        {
            return endpoint;
        }
        throw new InvalidOperationException($"No endpoint found for key '{key}'.");
    }
}