namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface IScrollable
{
    int CurrentIndex { get; }
    void MoveUp();
    void MoveDown();
    void ResetSelection();
}