using View.Entity.Structures;
using View.View;

namespace ShiftsLogger.Presentation.ShiftTypesMenu;

public sealed class ShiftTypesMenu : MenuView
{
    private const string Title = "Shift Types";
    public ShiftTypesMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Add Shift Type",
            entryAction: null,
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Manage Shift Types",
            entryAction: null,
            entryStyle: Settings?.Styles.Content
        )
    ];
}