namespace Shared.RequestFeatures;

public class PagedList<T> : List<T>
{
    public PaginationMetaData PaginationMetaData { get; set; }

    public PagedList(int count, int pageNumber, int pageSize, params IEnumerable<T> items)
    {
        PaginationMetaData = new PaginationMetaData
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            PageSize = pageSize,
            TotalCount = count
        };
        
        AddRange(items);
    }
}