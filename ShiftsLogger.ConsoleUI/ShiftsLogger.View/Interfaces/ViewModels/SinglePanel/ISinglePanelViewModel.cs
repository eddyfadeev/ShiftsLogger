using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface ISinglePanelViewModel : IScrollable, IReturnsEntry<IViewModelEntity>
{
    bool IsEmpty { get; }
    OnScreenMenuList<IViewModelEntity> PanelEntries { get; }
}