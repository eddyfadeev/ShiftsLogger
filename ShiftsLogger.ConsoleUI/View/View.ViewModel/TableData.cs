using Shared.RequestFeatures;
using Spectre.Console;
using View.Entity;
using View.Utility;
using View.Utility.Extensions;

namespace View.ViewModel;

public class TableData<T>
{
    private int _selectedIndex;
    
    public PagedList<T> ObjectsList { get; }
    public TableSettings Settings { get; set; }
    public StyledString[] TableColumnHeaders { get; private set; }
    public StyledString[][] TableDataRows { get; private set; }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            const int minIndex = 0;
            int maxIndex = ObjectsList.Count - 1;
            int previousSelection = _selectedIndex;

            _selectedIndex = Math.Clamp(value, minIndex, maxIndex);

            TableDataRows[previousSelection] = TableDataRows[previousSelection].RemoveStyle(Settings.ContentStyle);
            TableDataRows[_selectedIndex] = TableDataRows[_selectedIndex].ApplyStyle(Settings.SelectionStyle);
        }
    }

    public TableTitle Title { get; set; } = new (string.Empty);
    public TableTitle Footer { get; set; } = new (string.Empty);
    
    public TableData(PagedList<T> objectsList, TableSettings tableSettings) 
    {
        if (objectsList.Count <= 0)
        {
            throw new ArgumentException("No data provided to create a table data", nameof(objectsList));
        }
        
        Settings = tableSettings;
        ObjectsList = objectsList;
        TableColumnHeaders = GetColumnHeaders(objectsList[0]);
        TableDataRows = GetTableData(objectsList);
    }

    public bool RemoveColumn(string columnName)
    {
        StyledString? columnToRemove = Array.Find(
            TableColumnHeaders, s => 
            s.OriginString.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
        
        if (columnToRemove is null)
        {
            return false;
        }

        TableColumnHeaders = GetColumnHeaders(ObjectsList[0], columnToRemove);
        TableDataRows = GetTableData(ObjectsList, columnToRemove);

        return true;
    }

    private static StyledString[] GetColumnHeaders(T obj, params string[] columnsToIgnore) =>
        ClassDataExtractor
            .GetPropertyNames(obj)
            .Where(s => !columnsToIgnore.Contains(s))
            .Select(str => new StyledString(str.SplitCamelCase()))
            .ToArray();

    private StyledString[][] GetTableData(PagedList<T> pagedList, params string[] columnsToIgnore) =>
        pagedList.Select((item, index) =>
        {
            var dataRow = 
                ClassDataExtractor
                    .GetPropertyValuesAsString(item, columnsToIgnore)
                    .ToArray();

            return index == SelectedIndex
                ? dataRow.ApplyStyle(Settings.SelectionStyle)
                : dataRow.ApplyStyle(Settings.ContentStyle);
        }).ToArray();
}