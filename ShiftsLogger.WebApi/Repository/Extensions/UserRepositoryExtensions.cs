using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class UserRepositoryExtensions
{
    public static IQueryable<User> Sort(this IQueryable<User> users, UserParameters queryParameters)
    {
        if (string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            return users.OrderBy(u => u.FirstName);
        }
        
        var orderQuery = QueryBuilder.CreateOrderQuery<Location>(queryParameters.OrderBy);
        
        return string.IsNullOrWhiteSpace(orderQuery)        
            ? users.OrderBy(u => u.FirstName)         
            : users.OrderBy(orderQuery);
    }
}