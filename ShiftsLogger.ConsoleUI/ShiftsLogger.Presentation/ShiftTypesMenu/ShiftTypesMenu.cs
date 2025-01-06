using View.Entity;
using View.Menu;

namespace ShiftsLogger.Presentation.ShiftTypesMenu;

public sealed class ShiftTypesMenu : MenuViewBase<Menu, MenuEntry>
{
    public ShiftTypesMenu(IServiceProvider serviceProvider) 
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