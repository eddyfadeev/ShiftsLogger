using System.Globalization;
using System.Reflection;
using Shared.RequestFeatures;

namespace UriExtensions.Utility;

internal static class QueryBuilder
{
    public static string BuildParametersQuery<T>(T? entity)
        where T : RequestParameters
    {
        if (entity is null)
        {
            return string.Empty;
        }
        
        var properties = entity.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => 
                prop.PropertyType != typeof(bool) &&
                prop.GetValue(entity) is not null &&
                prop.GetValue(entity) != default &&
                !(prop.GetValue(entity) is string strValue && string.IsNullOrWhiteSpace(strValue)))
            .ToList();

        List<string> queryParameters = [];

        foreach (var property in properties)
        {
            var rawValue = property.GetValue(entity);
            
            string value = 
                rawValue is DateTime dateTime 
                    ? dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    : rawValue?.ToString() ?? $"{property.Name}: Error getting value!";
            
            queryParameters.Add($"{Uri.EscapeDataString(property.Name.ToLower())}={Uri.EscapeDataString(value)}");
        }

        return string.Join('&', queryParameters);
    }
}