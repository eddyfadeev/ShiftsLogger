using Spectre.Console;
using View.Entity.Models;
using View.Entity.Structures;

namespace View.Services;

public class MenuBuilderStrategy : TableBuilderStrategyBase<MenuEntry>
{
    public override Table Build(TableData<MenuEntry> tableData)
    {
        ValidateTableData(tableData);
        
        var table = InitializeTable(tableData);
        
        table.AddColumn
        (
            column: "Menu", 
            configure: col => col.Centered()
        );

        foreach (var menuEntry in tableData)
        {
            table.AddRow(menuEntry.EntryText);
        }
        
        return table;
    }
}