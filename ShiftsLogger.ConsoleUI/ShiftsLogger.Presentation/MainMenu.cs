using Microsoft.Extensions.DependencyInjection;
using View.Entity;
using View.Menu;
using View.Services.Contracts;

namespace ShiftsLogger.Presentation;

public sealed class MainMenu : MenuViewBase<Menu, MenuEntry>
{
    public MainMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    protected override Menu CreateMenu()
    {
        var tableBuilder = ServiceProvider.GetRequiredService<ITableBuilder>();
        
        ArgumentNullException.ThrowIfNull(tableBuilder, nameof(tableBuilder));
        ArgumentNullException.ThrowIfNull(Settings, nameof(Settings));
        ArgumentNullException.ThrowIfNull(MenuEntries, nameof(MenuEntries));
        
        return new Menu(tableBuilder, (MenuSettings)Settings, MenuEntries);
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Shifts",
            entryAction: CreateShiftsMenu(),
            entryStyle: Settings!.ContentStyle
        ),
        new
        (
            entryText: "Users",
            entryAction: CreateUsersMenu(),
            entryStyle: Settings!.ContentStyle
        ),
        new
        (
            entryText: "Locations",
            entryAction: CreateLocationsMenu(),
            entryStyle: Settings!.ContentStyle
        ),
        new
        (
            entryText: "Shift Types",
            entryAction: CreateShiftTypesMenu(),
            entryStyle: Settings!.ContentStyle
        ),
        new
        (
            entryText: "Exit",
            entryAction: MenuHandler.PopMenu,
            entryStyle: Settings!.ContentStyle
        ),
    ];

    protected override TableSettings CreateSettings() =>
        new MenuSettings();

    #region Actions

    private Action CreateShiftsMenu() =>
    () =>
    {
        var shiftsMenu = new ShiftsMenu.ShiftsMenu(ServiceProvider);
        MenuHandler.PushMenu(shiftsMenu);
    };

    private Action CreateUsersMenu() =>
    () =>
    {
        var usersMenu = new UsersMenu.UsersMenu(ServiceProvider);
        MenuHandler.PushMenu(usersMenu);
    };

    private Action CreateLocationsMenu() =>
    () =>
    {
        var locationsMenu = new LocationsMenu.LocationsMenu(ServiceProvider);
        MenuHandler.PushMenu(locationsMenu);
    };
        
    private Action CreateShiftTypesMenu() =>
    () =>
    {
        var shiftTypes = new ShiftTypesMenu.ShiftTypesMenu(ServiceProvider);
        MenuHandler.PushMenu(shiftTypes);
    };

    #endregion
}