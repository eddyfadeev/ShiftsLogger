namespace ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;

public class DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> : SinglePanelViewModel<TLeftPanelEntries> 
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    public int LastActiveSelectionIndex { get; private set; }
    public int RightPanelActiveIndex { get; private set; }
    public bool IsSinglePanelMode { get; private set; }
    public bool IsFilterSelected { get; private set; }
    
    public Dictionary<TLeftPanelEntries, List<TRightPanelEntries>> RightPanelEntries { get; }

    public DoublePanelViewModel(List<TLeftPanelEntries> leftPanelEntries, Dictionary<TLeftPanelEntries, List<TRightPanelEntries>> rightPanelEntries) : base(leftPanelEntries)
    {
        RightPanelEntries = rightPanelEntries;
        LastActiveSelectionIndex = -1;
        IsSinglePanelMode = true;
    }

    public void SelectLeftPanel() => IsSinglePanelMode = true;
    
    public void SelectRightPanel() => IsSinglePanelMode = false;
    
    public void ResetRightPanelSelection() => RightPanelActiveIndex = 0;

    public void ActivateFilter()
    {
        LastActiveSelectionIndex = SelectedEntryIndex;
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
            LastActiveSelectionIndex == - 1 
                ? 0 
                : RightPanelEntries[PanelEntries[LastActiveSelectionIndex]].Count - 1;

        if (IsSinglePanelMode)
        {
            return base.EnsureCorrectIndex(newIndex);
        }
        
        return newIndex <= upperBound &&
               newIndex >= lowerBound;
    }
    
}