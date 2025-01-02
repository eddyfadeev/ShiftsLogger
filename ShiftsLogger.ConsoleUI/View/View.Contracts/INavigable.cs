namespace View.Contracts;

public interface INavigable
{
    int SelectedIndex { get; }
    void SelectNext();
    void SelectPrevious();
    void ResetSelection();
}