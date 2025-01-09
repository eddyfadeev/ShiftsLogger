namespace View.Contracts;

public interface IPageable<in T>
{
    int CurrentPage { get; }
    int TotalPages { get; }
    void NextPage(IEnumerable<T> items);
    void PreviousPage(IEnumerable<T> items);
    void SetCurrentPage(IEnumerable<T> items, int page);
    void ResetPage(IEnumerable<T> items);
}