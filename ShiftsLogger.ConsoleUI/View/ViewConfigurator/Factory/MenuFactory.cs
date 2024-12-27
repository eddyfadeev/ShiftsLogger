using View.Contracts.Services;
using View.Entity.Structures;
using View.Menu;

namespace ViewConfigurator.Factory;

public class MenuFactory : IMenuFactory
{
    private readonly ITableBuilder _tableBuilder;

    public MenuFactory(ITableBuilder tableBuilder) =>
        _tableBuilder = tableBuilder;

    public NavigableMenu CreateNavigableMenu(MenuSettings settings, params List<MenuEntry> menuEntries) =>
        new(_tableBuilder, settings, menuEntries);

    public EntitiesMenu<T> CreateEntitiesMenu<T>(MenuSettings settings, params List<T> tableObjects) =>
        new(_tableBuilder, settings, tableObjects);
}