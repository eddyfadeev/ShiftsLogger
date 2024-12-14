namespace Shared.RequestFeatures;

public abstract class RequestParameters
{
    #region Constants

    private const int MinPageSize = 1;
    private const int MaxPageSize = 50;
    private const int DefaultPageSize = 10;
    
    private const int MinPageNumber = 1;

    #endregion

    #region Backing Fields

    private int _pageNumber = MinPageNumber;
    private int _pageSize = DefaultPageSize;
    private string _orderBy = string.Empty;
    private string _search = string.Empty;

    #endregion
    
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < MinPageNumber 
                            ? MinPageNumber 
                            : value;
    }
    
    public int PageSize
    {
        get => _pageSize;
        set => 
            _pageSize = value switch
            {
                < MinPageSize => MinPageSize,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
    }
    
    public string OrderBy
    {
        get => _orderBy;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            
            _orderBy = NormalizeOrderBy(value);
        }
    }

    public string? Search
    {
        get => _search;
        set => _search = string.IsNullOrWhiteSpace(value)
                        ? _search
                        : value;
    }

    private static string NormalizeOrderBy(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return string.Empty;
        }

        var orderDirection = ExtractOrderDirection(orderBy);

        var filteredArray = orderBy.Where(char.IsLetter).ToArray();
        
        var filteredQueryString = string.Join("", filteredArray).ToLower();
    
        return $"{filteredQueryString} {orderDirection}";
    }

    private static string ExtractOrderDirection(string orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return string.Empty;
        }
        
        string[] parts = orderBy.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        string orderDirection = parts.Length > 1 ? parts[^1].ToLower() : "asc";
        
        orderDirection = (orderDirection == "desc") ? "desc" : "asc";
        
        return orderDirection;
    }
}