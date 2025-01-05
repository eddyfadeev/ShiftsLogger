using System.Reflection;

namespace View.Utility;

public static class ClassDataExtractor
{
    private static readonly Dictionary<Type, PropertyInfo[]> PropertyCache = new();

    public static IEnumerable<string> GetPropertyNames<T>(T obj, params string[] propertiesToIgnore) =>
        obj is null 
            ? Array.Empty<string>() 
            : GetTypeProperties(obj)
                .Where(prop => 
                    !propertiesToIgnore.Contains(prop.Name, StringComparer.InvariantCultureIgnoreCase))
                .Select(prop => prop.Name);

    public static IEnumerable<string> GetPropertyValuesAsString<T>(T obj, params string[] propertiesToIgnore) =>
        obj is null 
            ? Array.Empty<string>() 
            : GetTypeProperties(obj)
                .Where(prop => 
                    !propertiesToIgnore.Contains(prop.Name, StringComparer.InvariantCultureIgnoreCase))
                .Select(prop => 
                    prop.GetValue(obj)?.ToString() 
                    ?? string.Empty);
    
    private static PropertyInfo[] GetTypeProperties<T>(T obj)
    {
        var type = obj!.GetType();
        if (!PropertyCache.TryGetValue(type, out var properties))
        {
            properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyCache[type] = properties;
        }
        
        return properties;
    }
}