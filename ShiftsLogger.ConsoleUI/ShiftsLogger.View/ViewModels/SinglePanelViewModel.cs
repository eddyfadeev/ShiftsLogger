namespace ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;

public class SinglePanelViewModel<TEntry>
    where TEntry : class
{
    public int SelectedEntryIndex { get; private set; }

    public List<TEntry> PanelEntries { get; }

    public SinglePanelViewModel(List<TEntry> entries)
    {
        PanelEntries = entries;
    }
    
    public void UpdateSelectionIndex(int newIndex)
    {
        if (!EnsureCorrectIndex(newIndex))
        {
            return;
        }
        
        SelectedEntryIndex = newIndex;
    }

    private protected virtual bool EnsureCorrectIndex(int newIndex)
    {
        const int lowerBound = 0;
        int upperBound = PanelEntries.Count - 1;

        return newIndex <= upperBound &&
               newIndex >= lowerBound;
    }
}