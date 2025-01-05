using View.Entity;
using View.Services;
using View.Services.Contracts;
using View.Utility.Extensions;

namespace View.Menu;

public class Menu : MenuBase<MenuEntry>
{
    public Menu(ITableBuilder tableBuilder, MenuSettings menuSettings, params IEnumerable<MenuEntry> tableEntries) 
        : base(tableBuilder, new MenuBuilderStrategy(), menuSettings, tableEntries)
    {
        MenuData[SelectedIndex] = 
            MenuData[SelectedIndex]
                .WithStyle(MenuData.Settings.SelectionStyle);
    }

    protected override void MoveCursor(int previousIndex, int newIndex)
    {
        MenuData[previousIndex] = 
            MenuData[previousIndex]
                .WithStyle(MenuData.Settings.ContentStyle);
        
        MenuData[newIndex] = 
            MenuData[newIndex]
                .WithStyle(MenuData.Settings.SelectionStyle);
    }
}