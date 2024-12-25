using Spectre.Console;
using View.Entity;

namespace View.Menu;

public sealed class MenuSettings : TableSettings
{
    public MenuSettings()
    {
        ShowColumnHeaders = false;
        ShowFooter = true;
        Expand = true;
        BorderStyle = TableBorder.Rounded;
        TitleStyle = new Style(foreground: Color.Olive);
        TitleAlignment = HorizontalAlignment.Center;
        HeadersStyle = null;
        ContentStyle = new Style(foreground: Color.White);
        ContentAlignment = HorizontalAlignment.Center;
        SelectionStyle = new Style(foreground: Color.Gold3_1);
        SeparatorRow = 0;
    }
}