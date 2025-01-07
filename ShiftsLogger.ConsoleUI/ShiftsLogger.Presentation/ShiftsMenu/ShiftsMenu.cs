using View.Entity.Structures;
using View.View;

namespace ShiftsLogger.Presentation.ShiftsMenu;

public sealed class ShiftsMenu : MenuView
{
    private const string Title = "Shifts";
    
    public ShiftsMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Add Shift",
            entryAction: null,
            entryStyle: Settings?.ContentStyle
        ),
        new
        (
            entryText: "Manage Shifts",
            entryAction: null,
            entryStyle: Settings?.ContentStyle
        )
    ];
}