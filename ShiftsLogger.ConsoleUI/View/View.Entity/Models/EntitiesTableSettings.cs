using Spectre.Console;

namespace View.Entity.Models;

public sealed class EntitiesTableSettings : TableSettings
{
    public EntitiesTableSettings(int totalPages)
    {
        ShowHeaders = true;
        ShowFooter = true;
        Expand = true;
        BorderStyle = TableBorder.Rounded;
        TitleStyle = new Style(foreground: Color.Olive);
        HeadersStyle = new Style(foreground: Color.White);
        FooterStyle = new Style(foreground: Color.White);
        ContentStyle = new Style(foreground: Color.White);
        SelectionStyle = new Style(foreground: Color.Gold3_1);
        SeparatorRow = 5;
        TotalPages = totalPages;
    }
    
    public int TotalPages { get; set; }
}