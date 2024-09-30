using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

namespace ShiftsLogger.View.ViewModels;

public class DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> 
    : IDoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries>
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    public int SelectedFilterIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }

    public bool IsFilterSelected { get; private set; }

    public SinglePanelViewModel<TLeftPanelEntries> LeftPanelViewModel { get; private set; }
    public SinglePanelViewModel<TRightPanelEntries> RightPanelViewModel { get; private set; }

    public DoublePanelViewModel(IEnumerable<TLeftPanelEntries> leftPanelEntriesList) : this(leftPanelEntriesList, []) {}
    
    public DoublePanelViewModel(IEnumerable<TLeftPanelEntries> leftPanelEntries, IEnumerable<TRightPanelEntries> rightPanelEntries)
    {
        LeftPanelViewModel = new SinglePanelViewModel<TLeftPanelEntries>(leftPanelEntries);
        RightPanelViewModel = new SinglePanelViewModel<TRightPanelEntries>(rightPanelEntries);
        SelectedFilterIndex = -1;
        IsSinglePanelMode = true;
    }
    
    public void SelectLeftPanel() => IsSinglePanelMode = true;
    
    public void SelectRightPanel() => IsSinglePanelMode = false;

    public void ResetLeftPanelSelection() =>
        LeftPanelViewModel.ResetSelection();
    
    public void ResetRightPanelSelection() =>
        RightPanelViewModel.ResetSelection();
    
    public void ActivateFilter()
    {
        SelectedFilterIndex = LeftPanelViewModel.CurrentIndex + 1;
        IsFilterSelected = true;
    }

    public void UpdateLeftPanelViewModel(IEnumerable<TLeftPanelEntries> leftPanelEntriesList) =>
        LeftPanelViewModel = new SinglePanelViewModel<TLeftPanelEntries>(leftPanelEntriesList);

    public void UpdateRightPanelViewModel(IEnumerable<TRightPanelEntries> rightPanelEntriesList) =>
        RightPanelViewModel = new SinglePanelViewModel<TRightPanelEntries>(rightPanelEntriesList);

    public TRightPanelEntries GetCurrentElement() => RightPanelViewModel.GetCurrentElement();
}