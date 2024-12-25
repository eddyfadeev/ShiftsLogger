using Spectre.Console;
using View.Entity;
using View.Utility.Extensions;

namespace View.ViewModel;

public abstract class TableData<T>
{
    private int _selectedIndex;
    
    protected TableData(List<T> objectsList, TableSettings tableSettings) 
    {
        if (objectsList.Count <= 0)
        {
            throw new ArgumentException("No data provided to create a table data", nameof(objectsList));
        }
        
        Settings = tableSettings ?? throw new ArgumentNullException(nameof(tableSettings), "Table settings cannot be null");
        ObjectsList = objectsList ?? throw new ArgumentNullException(nameof(objectsList), "Object list cannot be null");
    }
    
    public abstract StyledString[] TableColumnHeaders { get; protected set; }
    public abstract StyledString[][] TableDataRows { get; protected set; }
    
    public List<T> ObjectsList { get; }
    public TableSettings Settings { get; set; }

    public int SelectedIndex 
    {
        get => _selectedIndex;
        set
        {
            const int minIndex = 0;
            int maxIndex = ObjectsList.Count - 1;
            int previousIndex = _selectedIndex;

            int newIndex = Math.Clamp(value, minIndex, maxIndex);

            if (newIndex == previousIndex)
            {
                return;
            }

            _selectedIndex = newIndex; 

            TableDataRows[previousIndex] = 
                TableDataRows[previousIndex]
                    .RemoveStyle(Settings.ContentStyle);
            
            TableDataRows[_selectedIndex] = 
                TableDataRows[_selectedIndex]
                    .ApplyStyle(Settings.SelectionStyle);
        }
    }
    
    public TableTitle Title { get; set; } = new (string.Empty);
    public TableTitle Footer { get; set; } = new (string.Empty);
}