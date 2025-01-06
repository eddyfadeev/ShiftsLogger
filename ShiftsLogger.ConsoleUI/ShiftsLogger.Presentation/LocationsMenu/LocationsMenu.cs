using View.Entity;
using View.Menu;

namespace ShiftsLogger.Presentation.LocationsMenu;

public sealed class LocationsMenu : MenuViewBase<Menu, MenuEntry>
{
    public LocationsMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }
    protected override Menu CreateMenu()
    {
        throw new NotImplementedException();
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries()
    {
        throw new NotImplementedException();
    }
    
    protected override TableSettings CreateSettings() =>
        new MenuSettings();
}