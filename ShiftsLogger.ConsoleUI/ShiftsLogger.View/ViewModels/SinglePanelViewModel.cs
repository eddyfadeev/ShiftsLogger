using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

namespace ShiftsLogger.View.ViewModels;

public class SinglePanelViewModel : ISinglePanelViewModel
{
    public int CurrentIndex => PanelEntries.CurrentIndex;

    public OnScreenMenuList<IViewModelEntity> PanelEntries { get; }

    public SinglePanelViewModel(IEnumerable<IViewModelEntity> entries) => 
        PanelEntries = new OnScreenMenuList<IViewModelEntity>(entries);

    public void SelectPrevious() => PanelEntries.SelectPrevious();
    public void SelectNext() => PanelEntries.SelectNext();
    public void ResetSelection() => PanelEntries.ResetSelection();

    public IViewModelEntity GetCurrentElement() => PanelEntries.GetCurrentElement();
}