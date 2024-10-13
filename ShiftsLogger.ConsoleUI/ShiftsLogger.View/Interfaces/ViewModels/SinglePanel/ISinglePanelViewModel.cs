using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface ISinglePanelViewModel : IScrollable, IReturnsEntry<IViewModelEntity>
{
    OnScreenMenuList<IViewModelEntity> PanelEntries { get; }
}