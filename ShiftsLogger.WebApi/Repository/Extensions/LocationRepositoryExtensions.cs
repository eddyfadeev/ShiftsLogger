using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class LocationRepositoryExtensions
{
    public static IQueryable<Location> Sort(this IQueryable<Location> locations, LocationParameters queryParameters)
    {
        if (string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            return locations.OrderBy(l => l.Name);
        }
        
        var orderQuery = QueryBuilder.CreateOrderQuery<Location>(queryParameters.OrderBy);
        
        return string.IsNullOrWhiteSpace(orderQuery)        
            ? locations.OrderBy(l => l.Name)         
            : locations.OrderBy(orderQuery);
    }   
}