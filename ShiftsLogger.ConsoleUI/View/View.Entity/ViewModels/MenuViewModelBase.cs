using Spectre.Console;
using View.Contracts;
using View.Entity.Models;

namespace View.Entity.ViewModels;

public abstract class MenuViewModelBase<T> : ISelectable, IDisplayableData
    where T : IActionable
{
    private int _selectedIndex;
    
    public MenuViewModelBase(
        TableSettings tableSettings, 
        params IEnumerable<T> menuEntries)
    {
        MenuData = new TableData<T>(tableSettings, menuEntries);
    }

    public TableData<T> MenuData { get; }
    protected abstract void MoveCursor(int previousIndex, int newIndex);
    
    #region IDisplayableData

    public void SetTitle(string title) =>
        MenuData.Title = new TableTitle(title, MenuData.Settings.Styles.Title);
    
    public void SetFooter(string footer) =>
        MenuData.Footer = new TableTitle(footer, MenuData.Settings.Styles.Footer);

    #endregion
    
    #region ISelectable
    
    public int SelectedIndex 
    {
        get => _selectedIndex;
        private set
        {
            const int minIndex = 0;
            int maxIndex = MenuData.Count - 1 > 0 ? MenuData.Count - 1 : minIndex;
            int previousIndex = _selectedIndex;

            int newIndex = Math.Clamp(value, minIndex, maxIndex);

            if (newIndex == previousIndex)
            {
                return;
            }

            MoveCursor(previousIndex, newIndex);
            _selectedIndex = newIndex; 
        }
    }

    public void SelectNext() =>
        SelectedIndex++;

    public void SelectPrevious() =>
        SelectedIndex--;

    public void ResetSelection() =>
        SelectedIndex = 0;
    
    public void InvokeAction() =>
        MenuData[SelectedIndex].Action?.Invoke();
    
    #endregion
}