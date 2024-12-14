using System.Reflection;
using System.Text;

namespace Repository.Utility;

public static class QueryBuilder
{
    public static string CreateOrderQuery<T>(string orderByQuery)
    {
        string[] orderParameters = orderByQuery.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var queryBuilder = new StringBuilder();

        foreach (string parameter in orderParameters)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                continue;
            }

            string propertyFromQueryName = parameter.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(info =>
                info.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty is null)
            {
                continue;
            }

            string direction = parameter.EndsWith(" desc") ? "descending" : "ascending";

            queryBuilder.Append($"{objectProperty.Name} {direction}, ");
        }

        string orderQuery = queryBuilder.ToString().TrimEnd(',', ' ');

        return orderQuery;
    }
    
    public static string CreateSearchQuery<T>(string searchQuery)
    {
        var stringProperties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(string))
            .Select(p => p.Name)
            .ToList();

        if (!stringProperties.Any())
        {
            return string.Empty;
        }

        var predicate = stringProperties
            .Select(prop => $"({prop} != null && {prop}.ToLower().Contains(@0))")
            .Aggregate((a, b) => $"{a} || {b}");

        return predicate;
    }
}