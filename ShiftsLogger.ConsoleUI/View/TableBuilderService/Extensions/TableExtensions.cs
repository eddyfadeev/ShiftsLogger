using Spectre.Console;

namespace TableBuilderService.Extensions;

public static class TableExtensions
{
    public static Table ApplyDefaultConfiguration(this Table table)
    {
        table.RoundedBorder();
        table.ShowFooters();
        table.Expand();

        return table;
    }
}