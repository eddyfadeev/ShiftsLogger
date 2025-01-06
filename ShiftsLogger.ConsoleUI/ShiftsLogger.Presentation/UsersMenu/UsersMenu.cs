using View.Entity;
using View.Menu;

namespace ShiftsLogger.Presentation.UsersMenu;

public sealed class UsersMenu : MenuViewBase<Menu, MenuEntry>
{
    public UsersMenu(IServiceProvider serviceProvider) 
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