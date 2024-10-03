using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

namespace ShiftsLogger.View.ViewModels;

public class DoublePanelViewModel : IDoublePanelViewModel
{
    public int SelectedFilterIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }

    public bool IsFilterSelected { get; private set; }

    public SinglePanelViewModel LeftPanelViewModel { get; private set; }
    public SinglePanelViewModel RightPanelViewModel { get; private set; }

    public DoublePanelViewModel(IEnumerable<IViewModelEntity> leftPanelEntriesList) : this(leftPanelEntriesList, []) {}
    
    public DoublePanelViewModel(IEnumerable<IViewModelEntity> leftPanelEntries, IEnumerable<IViewModelEntity> rightPanelEntries)
    {
        LeftPanelViewModel = new SinglePanelViewModel(leftPanelEntries);
        RightPanelViewModel = new SinglePanelViewModel(rightPanelEntries);
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

    public void UpdateLeftPanelViewModel(IEnumerable<IViewModelEntity> leftPanelEntriesList) =>
        LeftPanelViewModel = new SinglePanelViewModel(leftPanelEntriesList);

    public void UpdateRightPanelViewModel(IEnumerable<IViewModelEntity> rightPanelEntriesList) =>
        RightPanelViewModel = new SinglePanelViewModel(rightPanelEntriesList);

    public IViewModelEntity GetCurrentElement() => RightPanelViewModel.GetCurrentElement();
}