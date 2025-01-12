using View.Entity.Structures;
using View.View;

namespace ShiftsLogger.Presentation.LocationsMenu;

public sealed class LocationsMenu : MenuView
{
    private const string Title = "Locations";
    
    public LocationsMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }
    
    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries() =>
    [
        new
        (
            entryText: "Add Location",
            entryAction: null,
            entryStyle: Settings?.Styles.Content
        ),
        new
        (
            entryText: "Manage Locations",
            entryAction: null,
            entryStyle: Settings?.Styles.Content
        )
    ];
}