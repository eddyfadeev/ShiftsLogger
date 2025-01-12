using View.Entity.Structures;
using View.View;

namespace ShiftsLogger.Presentation;

public sealed class MainMenu : MenuView
{
    private const string Title = "Main Menu";
    
    public MainMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }
    
    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Shifts",
            entryAction: CreateSubmenu<ShiftsMenu.ShiftsMenu>(),
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Users",
            entryAction: CreateSubmenu<UsersMenu.UsersMenu>(),
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Locations",
            entryAction: CreateSubmenu<LocationsMenu.LocationsMenu>(),
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Shift Types",
            entryAction: CreateSubmenu<ShiftTypesMenu.ShiftTypesMenu>(),
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Exit",
            entryAction: MenuHandler.PopMenu,
            entryStyle: Settings?.Styles.Content
        ),
    ];
}