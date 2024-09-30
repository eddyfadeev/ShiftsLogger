using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

public interface IDoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries>  
    :IDoublePanelScrollable, IEntriesFilterable, IReturnsEntry<TRightPanelEntries>
    where TLeftPanelEntries : class
    where TRightPanelEntries : class
{
    public SinglePanelViewModel<TLeftPanelEntries> LeftPanelViewModel { get; }
    public SinglePanelViewModel<TRightPanelEntries> RightPanelViewModel { get; }
    void UpdateLeftPanelViewModel(IEnumerable<TLeftPanelEntries> leftPanelEntriesList);
    void UpdateRightPanelViewModel(IEnumerable<TRightPanelEntries> rightPanelEntriesList);
}