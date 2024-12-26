using Spectre.Console;
using View.Contracts;
using View.Contracts.Services;
using View.Entity.Structures;
using View.ViewModel;

namespace View.Menu;

public abstract class Menu<T> : IMenu, ISelectable, IActionable
{
    protected readonly ITableBuilder TableBuilder;
    protected abstract List<MenuEntry> MenuEntries { get; init; }
    
    public abstract TableData<T> MenuTable { get; protected set; }
    public abstract MenuSettings Settings { get; protected set; }

    protected Menu(ITableBuilder tableBuilder)
    {
        TableBuilder = tableBuilder;
    }

    public virtual void DisplayMenu()
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
    
    public void InvokeAction() =>
        MenuEntries[MenuTable.SelectedIndex].Action?.Invoke();
}