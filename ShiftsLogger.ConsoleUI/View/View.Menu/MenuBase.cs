using Spectre.Console;
using View.Contracts;
using View.Entity;
using View.Services.Contracts;

namespace View.Menu;

public abstract class MenuBase<T> : ISelectable, IDisplayable, IDisplayableData
    where T : IActionable
{
    private int _selectedIndex;
    
    protected readonly ITableBuilder TableBuilder;
    protected readonly ITableBuilderStrategy<T> TableBuilderStrategy;
    
    public MenuBase(
        ITableBuilder tableBuilder,
        ITableBuilderStrategy<T> tableBuilderStrategy,
        TableSettings tableSettings, 
        params IEnumerable<T> menuEntries)
    {
        MenuData = new TableData<T>(tableSettings, menuEntries);
        TableBuilder = tableBuilder;
        TableBuilderStrategy = tableBuilderStrategy; 
    }
    
    protected TableData<T> MenuData { get; }
    protected abstract void MoveCursor(int previousIndex, int newIndex);

    #region IDisplayable
    
    public virtual void DisplayMenu()
    {
        var table = TableBuilder.Build(MenuData, TableBuilderStrategy);

        AnsiConsole.Write(table);
    }

    #endregion
    
    #region IDisplayableData

    public void SetTitle(string title) =>
        MenuData.Title = new TableTitle(title, MenuData.Settings.TitleStyle);
    
    public void SetFooter(string footer) =>
        MenuData.Footer = new TableTitle(footer, MenuData.Settings.FooterStyle);

    #endregion
    
    #region ISelectable
    
    public int SelectedIndex 
    {
        get => _selectedIndex;
        private set
        {
            const int minIndex = 0;
            int maxIndex = MenuData.Count - 1;
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