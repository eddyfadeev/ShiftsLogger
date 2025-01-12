using View.Events;

namespace View.Contracts;

public interface IPageable
{
    event ChangePageEvent? OnPageChanged;
    int CurrentPage { get; }
    void NextPage();
    void PreviousPage();
    void SubstituteData<T>(IEnumerable<T> data)
        where T : IActionable;
}