namespace ShiftsLogger.View.ViewModels;

public class SinglePanelViewModel<TEntry>
    where TEntry : class
{
    public int SelectedEntryIndex => PanelEntries.CurrentIndex;

    public OnScreenMenuList<TEntry> PanelEntries { get; }

    public SinglePanelViewModel(List<TEntry> entries)
    {
        PanelEntries = new OnScreenMenuList<TEntry>(entries);
    }

    public void MoveUp() => PanelEntries.MoveUp();
    public void MoveDown() => PanelEntries.MoveDown();

    public TEntry GetCurrentChoice() => PanelEntries.GetCurrentElement();
}