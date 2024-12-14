using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class ShiftTypesRepositoryExtensions
{
    public static IQueryable<ShiftType> Sort(this IQueryable<ShiftType> locations, ShiftTypeParameters queryParameters)
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