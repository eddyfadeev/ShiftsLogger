using View.Entity.Structures;
using View.Menu;

namespace ViewConfigurator.Factory;

public interface IMenuFactory
{
    NavigableMenu CreateNavigableMenu(MenuSettings settings, params List<MenuEntry> menuEntries);
    EntitiesMenu<T> CreateEntitiesMenu<T>(MenuSettings settings, params List<T> tableObjects);
}