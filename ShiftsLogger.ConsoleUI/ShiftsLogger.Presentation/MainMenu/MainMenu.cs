using Service.Contracts;
using View.Entity.Structures;
using View.Menu;
using ViewConfigurator.Factory;

namespace ShiftsLogger.Presentation.MainMenu;

public sealed class MainMenu : MenuBase<NavigableMenu>
{
    protected override List<MenuEntry> MenuEntries { get; }
    public override NavigableMenu Menu { get; }

    public MainMenu(IApiServiceManager apiServiceManager, IMenuFactory menuFactory)
        : base(apiServiceManager, menuFactory)
    {
        MenuEntries = GetMenuEntries();
        Menu = CreateMenu();
    }

    protected override NavigableMenu CreateMenu()
    {
        const string title = "Main Menu";
        
        var menuSettings = new MenuSettings
        {
            TitleStyle = StylingData.DefaultTitleStyling,
            ContentStyle = StylingData.DefaultMenuEntryStyling,
            SelectionStyle = StylingData.DefaultSelectionStyling
        };

        var menu = MenuFactory.CreateNavigableMenu(menuSettings, MenuEntries);
        menu.SetFooter(StaticData.InMenuFooter);
        menu.SetTitle(title);

        return menu;
    }

    protected override List<MenuEntry> GetMenuEntries() =>
    [
        new (
            name: "Shifts", 
            entryAction: () => 
                new ShiftsMenu(ApiServiceManager, MenuFactory).Menu.DisplayMenu(), 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Users", 
            entryAction: () => 
                new UsersMenu(ApiServiceManager, MenuFactory).Menu.DisplayMenu(), 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Locations", 
            entryAction: () => 
                new LocationsMenu(ApiServiceManager, MenuFactory).Menu.DisplayMenu(), 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Shift Types", 
            entryAction: () => 
                new ShiftTypesMenu(ApiServiceManager, MenuFactory).Menu.DisplayMenu(), 
            entryStyle: StylingData.DefaultMenuEntryStyling),
    ];
}

public sealed class ShiftsMenu : MenuBase<NavigableMenu>
{
    public ShiftsMenu(IApiServiceManager apiServiceManager, IMenuFactory menuFactory) 
        : base(apiServiceManager, menuFactory)
    {
        MenuEntries = GetMenuEntries();
        Menu = CreateMenu();
    }

    protected override List<MenuEntry> MenuEntries { get; }
    public override NavigableMenu Menu { get; }

    protected override NavigableMenu CreateMenu()
    {
        const string title = "Manage Shifts";
        
        var menuSettings = new MenuSettings
        {
            TitleStyle = StylingData.DefaultTitleStyling,
            ContentStyle = StylingData.DefaultMenuEntryStyling,
            SelectionStyle = StylingData.DefaultSelectionStyling
        };

        var menu = MenuFactory.CreateNavigableMenu(menuSettings, MenuEntries);
        menu.SetFooter(StaticData.InMenuFooter);
        menu.SetTitle(title);

        return menu;
    }

    protected override List<MenuEntry> GetMenuEntries() =>
    [
        new (
            name: "Add Shift", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Manage Shifts", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling)
    ];
}

public sealed class UsersMenu : MenuBase<NavigableMenu>
{
    public UsersMenu(IApiServiceManager apiServiceManager, IMenuFactory menuFactory) 
        : base(apiServiceManager, menuFactory)
    {
        MenuEntries = GetMenuEntries();
        Menu = CreateMenu();
    }

    protected override List<MenuEntry> MenuEntries { get; }
    public override NavigableMenu Menu { get; }

    protected override NavigableMenu CreateMenu()
    {
        const string title = "Manage Users";
        
        var menuSettings = new MenuSettings
        {
            TitleStyle = StylingData.DefaultTitleStyling,
            ContentStyle = StylingData.DefaultMenuEntryStyling,
            SelectionStyle = StylingData.DefaultSelectionStyling
        };

        var menu = MenuFactory.CreateNavigableMenu(menuSettings, MenuEntries);
        menu.SetFooter(StaticData.InMenuFooter);
        menu.SetTitle(title);

        return menu;
    }

    protected override List<MenuEntry> GetMenuEntries() =>
    [
        new (
            name: "Add User", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Manage Users", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling)
    ];
}

public sealed class LocationsMenu : MenuBase<NavigableMenu>
{
    public LocationsMenu(IApiServiceManager apiServiceManager, IMenuFactory menuFactory) 
        : base(apiServiceManager, menuFactory)
    {
        MenuEntries = GetMenuEntries();
        Menu = CreateMenu();
    }

    protected override List<MenuEntry> MenuEntries { get; }
    public override NavigableMenu Menu { get; }

    protected override NavigableMenu CreateMenu()
    {
        const string title = "Manage Locations";
        
        var menuSettings = new MenuSettings
        {
            TitleStyle = StylingData.DefaultTitleStyling,
            ContentStyle = StylingData.DefaultMenuEntryStyling,
            SelectionStyle = StylingData.DefaultSelectionStyling
        };

        var menu = MenuFactory.CreateNavigableMenu(menuSettings, MenuEntries);
        menu.SetFooter(StaticData.InMenuFooter);
        menu.SetTitle(title);

        return menu;
    }

    protected override List<MenuEntry> GetMenuEntries() =>
    [
        new (
            name: "Add Location", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Manage Locations", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling)
    ];
}

public sealed class ShiftTypesMenu : MenuBase<NavigableMenu>
{
    public ShiftTypesMenu(IApiServiceManager apiServiceManager, IMenuFactory menuFactory) 
        : base(apiServiceManager, menuFactory)
    {
        MenuEntries = GetMenuEntries();
        Menu = CreateMenu();
    }

    protected override List<MenuEntry> MenuEntries { get; }
    public override NavigableMenu Menu { get; }

    protected override NavigableMenu CreateMenu()
    {
        const string title = "Manage Shift Types";
        
        var menuSettings = new MenuSettings
        {
            TitleStyle = StylingData.DefaultTitleStyling,
            ContentStyle = StylingData.DefaultMenuEntryStyling,
            SelectionStyle = StylingData.DefaultSelectionStyling
        };

        var menu = MenuFactory.CreateNavigableMenu(menuSettings, MenuEntries);
        menu.SetFooter(StaticData.InMenuFooter);
        menu.SetTitle(title);

        return menu;
    }

    protected override List<MenuEntry> GetMenuEntries() =>
    [
        new (
            name: "Add Shift Type", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling),
        new (
            name: "Manage Shift Types", 
            /*TODO*/entryAction: null, 
            entryStyle: StylingData.DefaultMenuEntryStyling)
    ];
}