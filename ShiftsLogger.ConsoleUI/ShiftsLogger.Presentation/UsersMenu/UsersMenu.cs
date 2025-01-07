using View.Entity.Structures;
using View.View;

namespace ShiftsLogger.Presentation.UsersMenu;

public sealed class UsersMenu : MenuView
{
    private const string Title = "Users";
    
    public UsersMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Add User",
            entryAction: null,
            entryStyle: Settings?.ContentStyle
        ),
        new
        (
            entryText: "Manage Users",
            entryAction: null,
            entryStyle: Settings?.ContentStyle
        )
    ];
}