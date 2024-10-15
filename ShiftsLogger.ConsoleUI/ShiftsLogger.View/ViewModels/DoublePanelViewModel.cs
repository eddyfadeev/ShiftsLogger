using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.View.ViewModels;

public class DoublePanelViewModel : IDoublePanelViewModel
{
    public int SelectedFilterIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }

    public bool IsFilterSelected { get; private set; }

    public ISinglePanelViewModel LeftPanelViewModel { get; private set; }
    public ISinglePanelViewModel RightPanelViewModel { get; private set; }

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

    public void UpdateLeftPanelViewModel(ISinglePanelViewModel leftPanelModel) =>
        LeftPanelViewModel = leftPanelModel;

    public void UpdateRightPanelViewModel(ISinglePanelViewModel rightPanelModel) =>
        RightPanelViewModel = rightPanelModel;

    public IViewModelEntity GetCurrentElement() => RightPanelViewModel.GetCurrentElement();
}