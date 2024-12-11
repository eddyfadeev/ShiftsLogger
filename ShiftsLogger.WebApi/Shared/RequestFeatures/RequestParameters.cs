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
        set => _orderBy = string.IsNullOrWhiteSpace(value) 
                        ? _orderBy 
                        : value;
    }
}