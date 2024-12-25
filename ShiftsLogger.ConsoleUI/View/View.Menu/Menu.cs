using Spectre.Console;
using View.Contracts;
using View.Contracts.Services;
using View.Entity;
using View.ViewModel;

namespace View.Menu;

public abstract class Menu : IMenu
{
    protected ITableBuilder TableBuilder;
    
    public abstract MenuTable MenuTable { get; protected set; }
    public abstract MenuSettings Settings { get; protected set; }

    protected Menu(ITableBuilder tableBuilder)
    {
        TableBuilder = tableBuilder;
    }
    
    public abstract void DisplayMenu();
}

public sealed class NavigableMenu : Menu, ISelectable
{
    private List<MenuEntry> _menuEntries;
    
    public NavigableMenu(ITableBuilder tableBuilder, MenuSettings settings, List<MenuEntry> menuEntries) 
        : base(tableBuilder)
    {
        _menuEntries = menuEntries;
        Settings = settings;
        MenuTable = new MenuTable(
            menuEntries.Select(e => 
                e.Name).ToList()
            , settings);
    }

    public override MenuTable MenuTable { get; protected set; }
    public override MenuSettings Settings { get; protected set; }

    public override void DisplayMenu()
    {
        var table = TableBuilder.Build(MenuTable);
        
        AnsiConsole.Write(table);
    }
    
    public void SelectNext() =>
        MenuTable.SelectedIndex++;

    public void SelectPrevious() =>
        MenuTable.SelectedIndex--;

    public void ResetSelection() =>
        MenuTable.SelectedIndex = 0;
}