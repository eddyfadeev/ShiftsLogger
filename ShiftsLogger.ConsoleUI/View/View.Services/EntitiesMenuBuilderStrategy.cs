using Spectre.Console;
using Spectre.Console.Rendering;
using View.Entity.Models;
using View.Entity.Structures;

namespace View.Services;

public class EntitiesMenuBuilderStrategy : TableBuilderStrategyBase<EntityEntryData>
{
    public override Table Build(TableData<EntityEntryData> tableData)
    {
        ValidateTableData(tableData);
        
        var table = InitializeTable(tableData);
        var tableEntity = tableData.First();

        table.AddColumns(
            tableEntity.Headers.Select(header => 
                new TableColumn(header)
        ).ToArray());

        for (int i = 0; i < tableData.Count; i++)
        {
            if (i is not 0 
                && tableData.Settings.RowSeparatorIsActive 
                && i % tableData.Settings.SeparatorRow is 0)
            {
                table.AddEmptyRow();
            }

            IEnumerable<IRenderable> rowData = tableData[i].EntryData.Select(entryData => entryData);
            table.AddRow(rowData);
        }

        return table;
    }
}