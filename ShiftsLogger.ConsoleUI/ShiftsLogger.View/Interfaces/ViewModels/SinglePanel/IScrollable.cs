namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface IScrollable
{
    int CurrentIndex { get; }
    void SelectPrevious();
    void SelectNext();
    void ResetSelection();
}