using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.DoublePanel;

public interface IDoublePanelViewModel  
    :IDoublePanelScrollable, IEntriesFilterable, IReturnsEntry<IViewModelEntity>
{
    public ISinglePanelViewModel LeftPanelViewModel { get; }
    public ISinglePanelViewModel RightPanelViewModel { get; }
    void UpdateLeftPanelViewModel(ISinglePanelViewModel leftPanelModel);
    void UpdateRightPanelViewModel(ISinglePanelViewModel rightPanelModel);
}