using Spectre.Console;
using TableBuilderService.Extensions;
using TableBuilderService.Utility;
using View.Contracts;

namespace TableBuilderService;

public class TableBuilder : ITableBuilder
{
    public Table Build<T>(params List<T> tableData)
    {
        if (tableData.Count < 1)
        {
            throw new ArgumentException("No data provided to build a table", nameof(tableData));
        }
        
        var table = new Table();
        table.ApplyDefaultConfiguration();
        
        var columnNames = ClassDataExtractor.GetPropertyNames(tableData[0]).ToArray();
        table.AddColumns(columnNames);

        for (int i = 0; i < tableData.Count; i++)
        {
            if (i is not 0 && i % 5 is 0)
            {
                table.AddEmptyRow();
            }
            
            var data = ClassDataExtractor.GetPropertyValuesAsString(tableData[i]).ToArray();
            
            table.AddRow(data);
        }

        var sortKeys = columnNames.Select((name, index) => $"({index + 1}) - {name}");
        
        // TODO: Extract this into a separate class or method
        var footer = $"""
                      [white]
                      Press ESC to return to previous menu
                      Press right arrow to view next page or left arrow to return to the previous page
                      Press (key) to toggle sorting
                      {string.Join(" | ", sortKeys)}
                      [/]
                      """;
        
        table.Caption(new TableTitle(footer));

        return table;
    }
}