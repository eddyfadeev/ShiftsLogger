using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface ISinglePanelViewModel<TEntity> : IScrollable, IReturnsEntry<TEntity>
    where TEntity : notnull
{
    OnScreenMenuList<TEntity> PanelEntries { get; }
}