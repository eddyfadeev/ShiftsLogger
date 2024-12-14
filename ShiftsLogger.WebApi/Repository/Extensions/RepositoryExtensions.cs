using System.Linq.Dynamic.Core;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class RepositoryExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, RequestParameters queryParameters) =>
        queryable
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);
    
    public static IQueryable<T> Search<T>(this IQueryable<T> query, RequestParameters queryParameters)
    {
        if (string.IsNullOrEmpty(queryParameters.Search))
        {
            return query;
        }

        var searchQuery = queryParameters.Search.Trim().ToLower();
        var searchPredicate = QueryBuilder.CreateSearchQuery<T>(searchQuery);

        return query.Where(searchPredicate, searchQuery);
    }
}