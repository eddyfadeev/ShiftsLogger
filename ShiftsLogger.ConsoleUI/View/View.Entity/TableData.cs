using Shared.RequestFeatures;
using Spectre.Console;
using Utility;
using Utility.Extensions;

namespace View.Entity;

public class TableData<T>
{
    private int _selectedIndex;
    
    public PagedList<T> ObjectsList { get; }
    public TableSettings Settings { get; set; }
    public string[] TableColumnHeaders { get; private set; }
    public string[][] TableDataRows { get; private set; }


    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            const int minIndex = 0;
            int maxIndex = ObjectsList.Count - 1;
            int previousSelection = _selectedIndex;

            _selectedIndex = Math.Clamp(value, minIndex, maxIndex);
            
            TableDataRows[previousSelection] = TableDataRows[previousSelection].DecolorizeSelectedOption();
            TableDataRows[_selectedIndex] = TableDataRows[_selectedIndex].ColorizeSelectedOption();
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
        TableColumnHeaders = ExtractColumnHeaders(objectsList[0]);
        TableDataRows = ProcessTableData(objectsList);
    }

    public bool RemoveColumn(string columnName)
    {
        var columnToRemove = Array.Find(
            TableColumnHeaders, s => 
            s.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));
        
        if (columnToRemove is null)
        {
            return false;
        }

        TableColumnHeaders = ExtractColumnHeaders(ObjectsList[0], columnToRemove);
        TableDataRows = ProcessTableData(ObjectsList, columnToRemove);

        return true;
    }

    private static string[] ExtractColumnHeaders(T obj, params string[] columnsToIgnore) =>
        ClassDataExtractor
            .GetPropertyNames(obj)
            .Where(s => !columnsToIgnore.Contains(s))
            .ToArray()
            .SplitCamelCase();

    private string[][] ProcessTableData(PagedList<T> pagedList, params string[] columnsToIgnore) =>
        pagedList.Select((item, index) =>
        {
            var dataRow = ClassDataExtractor.GetPropertyValuesAsString(item, columnsToIgnore).ToArray();
            return index == SelectedIndex 
                ? dataRow.ColorizeSelectedOption() 
                : dataRow;
        }).ToArray();
}