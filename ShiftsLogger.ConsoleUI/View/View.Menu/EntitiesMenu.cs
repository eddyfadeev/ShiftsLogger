using View.Contracts.Services;
using View.Entity.Structures;
using View.ViewModel;

namespace View.Menu;

public sealed class EntitiesMenu<T> : Menu<T>
{
    public EntitiesMenu(ITableBuilder tableBuilder, MenuSettings settings, params List<T> tableObjects) 
        : base(tableBuilder)
    {
        Settings = settings;
        MenuTable = new EntitiesTable<T>(tableObjects, settings);
        MenuEntries = tableObjects.Select(obj =>
            new MenuEntry(
                obj?.ToString() 
                ?? throw new InvalidOperationException("Problem creating entries map"))
        ).ToList();
    }

    protected override List<MenuEntry> MenuEntries { get; init; }
    public override TableData<T> MenuTable { get; protected set; }
    public override MenuSettings Settings { get; protected set; }
}