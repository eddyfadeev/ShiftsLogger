using Spectre.Console;
using View.Contracts;
using View.Entity;

namespace TableBuilderService;

public class TableBuilder : ITableBuilder
{
    public Table Build<T>(TableData<T> tableData)
    {
        if (tableData.ObjectsList.Count < 1)
        {
            throw new ArgumentException("No data provided to build a table", nameof(tableData));
        }
        
        var table = new Table
        {
            ShowHeaders = tableData.Settings.ShowTitle,
            ShowFooters = tableData.Settings.ShowFooter,
            Expand = tableData.Settings.Expand,
            Title = tableData.Title,
            Caption = tableData.Footer
        };
        
        table.Border(tableData.Settings.Border);
        table.AddColumns(tableData.TableColumnHeaders);
        
        for (int i = 0; i < tableData.TableDataRows.Length; i++)
        {
            if (i is not 0 && i % tableData.Settings.SeparatorRow is 0)
            {
                table.AddEmptyRow();
            }
            
            table.AddRow(tableData.TableDataRows[i]);
        }

        return table;
    }
}