namespace ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

public interface IEntriesFilterable
{
    int SelectedFilterIndex { get; }
    bool IsFilterSelected { get; }
    void ActivateFilter();
}