using View.Contracts.Services;
using View.Entity.Structures;
using View.ViewModel;

namespace View.Menu;

public sealed class NavigableMenu : Menu<StyledString>
{
    public NavigableMenu(ITableBuilder tableBuilder, MenuSettings settings, params List<MenuEntry> menuEntries) 
        : base(tableBuilder)
    {
        MenuEntries = menuEntries;
        Settings = settings;
        MenuTable = new MenuTable(
            menuEntries.Select(e => 
                e.Name).ToList()
            , settings);
    }

    protected override List<MenuEntry> MenuEntries { get; init; }
    public override TableData<StyledString> MenuTable { get; protected set; }
    public override MenuSettings Settings { get; protected set; }
}