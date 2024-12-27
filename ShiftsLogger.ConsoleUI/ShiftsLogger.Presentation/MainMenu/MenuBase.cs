using Service.Contracts;
using View.Contracts;
using View.Entity.Structures;
using ViewConfigurator.Factory;

namespace ShiftsLogger.Presentation.MainMenu;

public abstract class MenuBase<T>
    where T : ISelectable, IActionable, IMenu
{
    protected readonly IApiServiceManager ApiServiceManager;
    protected readonly IMenuFactory MenuFactory;

    protected abstract List<MenuEntry> MenuEntries { get; }

    public abstract T Menu { get; }
    
    protected MenuBase(IApiServiceManager apiServiceManager, IMenuFactory menuFactory)
    {
        ApiServiceManager = apiServiceManager;
        MenuFactory = menuFactory;
    }

    protected abstract T CreateMenu();
    protected abstract List<MenuEntry> GetMenuEntries();
}