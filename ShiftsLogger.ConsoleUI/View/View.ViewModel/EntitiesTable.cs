using View.Entity;
using View.Entity.Structures;
using View.Utility;
using View.Utility.Extensions;

namespace View.ViewModel;

public sealed class EntitiesTable<T> : TableData<T>
{
    public override StyledString[] TableColumnHeaders { get; protected set; }
    public override StyledString[][] TableDataRows { get; protected set; }
    
    public EntitiesTable(List<T> objectsList, TableSettings tableSettings) 
        : base(objectsList, tableSettings)
    {
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

    private StyledString[][] GetTableData(IEnumerable<T> pagedList, params string[] columnsToIgnore) =>
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