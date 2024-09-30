using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.View.ViewModels;

public class SinglePanelViewModel<TEntry> : ISinglePanelViewModel<TEntry>
    where TEntry : class
{
    public int CurrentIndex => PanelEntries.CurrentIndex;

    public OnScreenMenuList<TEntry> PanelEntries { get; }

    public SinglePanelViewModel(IEnumerable<TEntry> entries)
    {
        PanelEntries = new OnScreenMenuList<TEntry>(entries);
    }

    public void MoveUp() => PanelEntries.MoveUp();
    public void MoveDown() => PanelEntries.MoveDown();
    public void ResetSelection() => PanelEntries.ResetSelection();

    public TEntry GetCurrentElement() => PanelEntries.GetCurrentElement();
}