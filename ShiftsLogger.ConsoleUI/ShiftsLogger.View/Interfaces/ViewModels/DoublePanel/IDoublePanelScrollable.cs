namespace ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

public interface IDoublePanelScrollable
{
    bool IsSinglePanelMode { get; }
    void SelectLeftPanel();
    public void SelectRightPanel();
    void ResetLeftPanelSelection();
    void ResetRightPanelSelection();
}