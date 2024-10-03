using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

public interface IDoublePanelViewModel  
    :IDoublePanelScrollable, IEntriesFilterable, IReturnsEntry<IViewModelEntity>
{
    public SinglePanelViewModel LeftPanelViewModel { get; }
    public SinglePanelViewModel RightPanelViewModel { get; }
    void UpdateLeftPanelViewModel(IEnumerable<IViewModelEntity> leftPanelEntriesList);
    void UpdateRightPanelViewModel(IEnumerable<IViewModelEntity> rightPanelEntriesList);
}