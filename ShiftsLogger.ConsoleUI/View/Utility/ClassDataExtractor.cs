using System.Reflection;

namespace Utility;

public static class ClassDataExtractor
{
    private static readonly Dictionary<Type, PropertyInfo[]> PropertyCache = new();

    public static IEnumerable<string> GetPropertyNames<T>(T obj) =>
        obj is null 
            ? Array.Empty<string>() 
            : GetTypeProperties(obj).Select(prop => prop.Name);

    public static IEnumerable<string> GetPropertyValuesAsString<T>(T obj, params string[] columnsToIgnore) =>
        obj is null 
            ? Array.Empty<string>() 
            : GetTypeProperties(obj)
                .Where(prop => !columnsToIgnore.Contains(prop.Name))
                .Select(prop => prop.GetValue(obj)?.ToString() ?? string.Empty);
    
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