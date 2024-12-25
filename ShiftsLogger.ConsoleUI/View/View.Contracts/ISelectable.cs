namespace View.Contracts;

public interface ISelectable
{
    void SelectNext();
    void SelectPrevious();
    void ResetSelection();
}