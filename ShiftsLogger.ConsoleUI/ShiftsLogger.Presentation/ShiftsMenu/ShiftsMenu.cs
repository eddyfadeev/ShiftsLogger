using Microsoft.Extensions.DependencyInjection;
using View.Entity;
using View.Menu;
using View.Services.Contracts;

namespace ShiftsLogger.Presentation.ShiftsMenu;

public sealed class ShiftsMenu : MenuViewBase<Menu, MenuEntry>
{
    public ShiftsMenu(IServiceProvider serviceProvider) 
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
            entryText: "Add Shift",
            entryAction: AddShift(),
            entryStyle: Settings!.ContentStyle
        ),
        new
        (
            entryText: "Manage Shifts",
            entryAction: ManageShifts(),
            entryStyle: Settings!.ContentStyle
        )
    ];

    protected override TableSettings CreateSettings() =>
        new MenuSettings();

    #region Actions

    private Action AddShift() =>
        () =>
        {

        };

    private Action ManageShifts() =>
        () =>
        {

        };

    #endregion
}