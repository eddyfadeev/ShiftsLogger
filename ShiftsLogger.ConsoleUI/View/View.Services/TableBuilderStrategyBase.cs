using Spectre.Console;
using View.Entity;
using View.Services.Contracts;

namespace View.Services;

public abstract class TableBuilderStrategyBase<T> : ITableBuilderStrategy<T>
{
    public abstract Table Build(TableData<T> tableData);
    
    protected Table InitializeTable(TableData<T> tableData) =>
        new ()
        {
            Border = tableData.Settings.BorderStyle,
            ShowHeaders = tableData.Settings.ShowHeaders,
            ShowFooters = tableData.Settings.ShowFooter,
            Expand = tableData.Settings.Expand,
            Title = tableData.Title,
            Caption = tableData.Footer
        };

    protected void ValidateTableData(TableData<T> tableData)
    {
        ArgumentNullException.ThrowIfNull(tableData, nameof(tableData));
        ArgumentNullException.ThrowIfNull(tableData.Settings, nameof(tableData.Settings));

        if (tableData.Count < 1)
        {
            throw new ArgumentException("Table data must contain at least one item.", nameof(tableData));
        }
        
        ArgumentNullException.ThrowIfNull(tableData.FirstOrDefault());
    }
}
