using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.View.ViewModels;

public class SinglePanelViewModel : ISinglePanelViewModel
{
    public int CurrentIndex => PanelEntries.CurrentIndex;

    public OnScreenMenuList<IViewModelEntity> PanelEntries { get; }

    public SinglePanelViewModel(IEnumerable<IViewModelEntity> entries)
    {
        entries = entries.ToList();
        
        PanelEntries = new OnScreenMenuList<IViewModelEntity>(entries);
    }

    public void MoveUp() => PanelEntries.MoveUp();
    public void MoveDown() => PanelEntries.MoveDown();
    public void ResetSelection() => PanelEntries.ResetSelection();

    public IViewModelEntity GetCurrentElement() => PanelEntries.GetCurrentElement();
}