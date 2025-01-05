namespace View.Contracts;

public interface ISelectable
{
    int SelectedIndex { get; }
    void SelectNext();
    void SelectPrevious();
    void ResetSelection();
    void InvokeAction();
}