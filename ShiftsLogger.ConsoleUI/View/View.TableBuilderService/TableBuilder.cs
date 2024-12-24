using Spectre.Console;
using View.Contracts;
using View.ViewModel;

namespace View.TableBuilderService;

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
            ShowHeaders = tableData.Settings.ShowColumnHeaders,
            ShowFooters = tableData.Settings.ShowFooter,
            Expand = tableData.Settings.Expand,
            Title = tableData.Title,
            Caption = tableData.Footer
        };
        
        table.Border(tableData.Settings.BorderStyle);

        var colHeaders = 
            tableData.TableColumnHeaders
                .Select(head => 
                    new TableColumn(head.StyledText))
                .ToArray();
        
        table.AddColumns(colHeaders);
        
        for (int i = 0; i < tableData.TableDataRows.Length; i++)
        {
            if (i is not 0 
                && tableData.Settings.RowSeparatorIsActive 
                && i % tableData.Settings.SeparatorRow is 0)
            {
                table.AddEmptyRow();
            }
            
            table.AddRow(
                tableData.TableDataRows[i]
                    .Select(r => r.StyledText)
                );
        }

        return table;
    }
}