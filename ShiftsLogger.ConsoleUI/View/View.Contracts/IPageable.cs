namespace View.Contracts;

public interface IPageable<in T>
{
    int CurrentPage { get; }
    int TotalPages { get; }
    void NextPage(params IEnumerable<T> items);
    void PreviousPage(params IEnumerable<T> items);
    void SetCurrentPage(int page, params IEnumerable<T> items);
    void ResetPage(params IEnumerable<T> items);
}