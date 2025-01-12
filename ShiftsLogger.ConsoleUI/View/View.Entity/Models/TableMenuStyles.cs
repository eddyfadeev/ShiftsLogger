using Spectre.Console;

namespace View.Entity.Models;

public class TableMenuStyles : MenuStyles
{
    public TableBorder BorderStyle { get; set; } = TableBorder.Rounded;
    public Style TableHeader { get; set; } = new(foreground: Color.White);
}