using System.Reflection;

namespace TableBuilderService.Utility;

public static class ClassDataExtractor
{
    public static IEnumerable<string> GetPropertyNames<T>(T obj) =>
        obj?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(prop => prop.Name) ?? 
        Array.Empty<string>();

    public static IEnumerable<string> GetPropertyValuesAsString<T>(T obj) =>
        obj?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(prop =>
            prop.GetValue(obj)?.ToString() ?? string.Empty) ?? Array.Empty<string>();
}