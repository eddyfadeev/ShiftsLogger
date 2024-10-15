using ShiftsLogger.Domain.Enums;
using ShiftsLogger.View.Interfaces.ViewModels;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class DetailedViewVIewModel : IDetailsViewModel
{
    public Panel TopPanel { get; private set; }
    public OnScreenMenuList<IViewModelEntity> PanelEntries { get; private set; }
    public int CurrentIndex => PanelEntries.CurrentIndex;

    public DetailedViewVIewModel(IViewModelEntity entity)
    {
        TopPanel = new Panel(entity.GetDetailedRepresentation());
        TopPanel.Border(BoxBorder.None);
        TopPanel.Expand = false;
        PanelEntries = new (InitializeMenuEntries());
    }
    
    public void SelectPrevious() => 
        PanelEntries.SelectPrevious();

    public void SelectNext() =>
        PanelEntries.SelectNext();

    public void ResetSelection() =>
        PanelEntries.ResetSelection();

    private static IEnumerable<IViewModelEntity> InitializeMenuEntries()
    {
        const int elHeight = 1;
        var list = new List<MenuEntry>();
        
        foreach (var entry in Enum.GetValues(typeof(EditOptions)))
        {
            list.Add(new MenuEntry(entry.ToString(), elHeight));
        }
        
        return new List<IViewModelEntity>(list);
    }

    public IViewModelEntity GetCurrentElement() =>
        PanelEntries.GetCurrentElement();
}

public interface IDetailsViewModel : ISinglePanelViewModel
{
    Panel TopPanel { get; }
}

