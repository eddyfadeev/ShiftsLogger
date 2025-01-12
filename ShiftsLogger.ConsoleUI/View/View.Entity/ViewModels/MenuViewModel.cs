using View.Entity.Extensions;
using View.Entity.Models;
using View.Entity.Structures;

namespace View.Entity.ViewModels;

public class MenuViewModel : MenuViewModelBase<MenuEntry>
{
    public MenuViewModel(MenuSettings menuSettings, params IEnumerable<MenuEntry> tableEntries) 
        : base(menuSettings, tableEntries)
    {
        MenuData[SelectedIndex] = 
            MenuData[SelectedIndex]
                .WithStyle(MenuData.Settings.Styles.Selection);
    }

    protected override void MoveCursor(int previousIndex, int newIndex)
    {
        MenuData[previousIndex] = 
            MenuData[previousIndex]
                .WithStyle(MenuData.Settings.Styles.Content);
        
        MenuData[newIndex] = 
            MenuData[newIndex]
                .WithStyle(MenuData.Settings.Styles.Selection);
    }
}