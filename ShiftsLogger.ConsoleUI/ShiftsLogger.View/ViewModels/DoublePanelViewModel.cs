namespace ShiftsLogger.View.ViewModels;

public class DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries>
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    public int SelectedFilterIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }

    public bool IsFilterSelected { get; private set; }

    public SinglePanelViewModel<TLeftPanelEntries> LeftPanelViewModel { get; private set; }
    public SinglePanelViewModel<TRightPanelEntries> RightPanelViewModel { get; private set; }

    public DoublePanelViewModel(List<TLeftPanelEntries> leftPanelEntriesList) : this(leftPanelEntriesList, []) {}
    
    public DoublePanelViewModel(List<TLeftPanelEntries> leftPanelEntries, List<TRightPanelEntries> rightPanelEntries)
    {
        LeftPanelViewModel = new SinglePanelViewModel<TLeftPanelEntries>(leftPanelEntries);
        RightPanelViewModel = new SinglePanelViewModel<TRightPanelEntries>(rightPanelEntries);
        SelectedFilterIndex = -1;
        IsSinglePanelMode = true;
    }
    
    public void SelectLeftPanel() => IsSinglePanelMode = true;
    
    public void SelectRightPanel() => IsSinglePanelMode = false;
    
    public void UpdateRightPanelIndex(int newIndex) =>
        RightPanelViewModel.UpdateSelectionIndex(newIndex);

    public void UpdateLeftPanelIndex(int newIndex) =>
        LeftPanelViewModel.UpdateSelectionIndex(newIndex);

    public void ResetRightPanelSelection() => 
        RightPanelViewModel.UpdateSelectionIndex(0);
    
    public void ActivateFilter()
    {
        SelectedFilterIndex = LeftPanelViewModel.SelectedEntryIndex + 1;
        IsFilterSelected = true;
    }

    public void UpdateLeftPanelViewModel(List<TLeftPanelEntries> leftPanelEntriesList) =>
        LeftPanelViewModel = new SinglePanelViewModel<TLeftPanelEntries>(leftPanelEntriesList);

    public void UpdateRightPanelViewModel(List<TRightPanelEntries> rightPanelEntriesList) =>
        RightPanelViewModel = new SinglePanelViewModel<TRightPanelEntries>(rightPanelEntriesList);

    public List<TRightPanelEntries> GetRightPanelEntriesList()
    {
        if (SelectedFilterIndex < 0)
        {
            return [];
        }

        return RightPanelViewModel.PanelEntries;
    }
}
/*public class DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> : SinglePanelViewModel<TLeftPanelEntries> 
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    public int SelectedFilterIndex { get; private set; }
    public int RightPanelActiveIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }
    public bool IsFilterSelected { get; private set; }
    
    public Dictionary<TLeftPanelEntries, List<TRightPanelEntries>> RightPanelEntries { get; }

    public DoublePanelViewModel(List<TLeftPanelEntries> leftPanelEntries, Dictionary<TLeftPanelEntries, List<TRightPanelEntries>> rightPanelEntries) : base(leftPanelEntries)
    {
        RightPanelEntries = rightPanelEntries;
        SelectedFilterIndex = -1;
        IsSinglePanelMode = true;
    }

    public void SelectLeftPanel() => IsSinglePanelMode = true;
    
    public void SelectRightPanel() => IsSinglePanelMode = false;
    
    public void ResetRightPanelSelection() => RightPanelActiveIndex = 0;

    public void ActivateFilter()
    {
        SelectedFilterIndex = SelectedEntryIndex;
        IsFilterSelected = true;
    }

    public void UpdateRightPanelIndex(int newIndex)
    {
        if (!EnsureCorrectIndex(newIndex))
        {
            return;
        }
        
        RightPanelActiveIndex = newIndex;
    }

    public void UpdateLeftPanelIndex(int newIndex)
    {
        UpdateSelectionIndex(newIndex);
    }
    
    private protected override bool EnsureCorrectIndex(int newIndex)
    {
        const int lowerBound = 0;
        int upperBound = 
            SelectedFilterIndex == - 1 
                ? 0 
                : RightPanelEntries[PanelEntries[SelectedFilterIndex]].Count - 1;

        if (IsSinglePanelMode)
        {
            return base.EnsureCorrectIndex(newIndex);
        }
        
        return newIndex <= upperBound &&
               newIndex >= lowerBound;
    }
    
}*/