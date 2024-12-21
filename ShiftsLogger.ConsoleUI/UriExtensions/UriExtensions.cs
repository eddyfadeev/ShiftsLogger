using Shared.RequestFeatures;
using UriExtensions.Utility;

namespace UriExtensions;

public static class UriExtensions
{
    public static Uri AppendGuid(this Uri uri, Guid guid) =>
        new ($"{uri.OriginalString.TrimEnd('/')}/{guid}", UriKind.Relative);

    public static Uri AddQueryParameters<T>(this Uri uri, T? entity) where T : RequestParameters =>
        new($"{uri.OriginalString}?{QueryBuilder.BuildParametersQuery(entity)}", UriKind.Relative);
}