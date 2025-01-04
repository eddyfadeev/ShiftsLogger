using Spectre.Console;

namespace View.Entity;

public sealed class MenuSettings : TableSettings
{
    public MenuSettings()
    {
        ShowHeaders = false;
        ShowFooter = true;
        Expand = true;
        BorderStyle = TableBorder.Rounded;
        TitleStyle = new Style(foreground: Color.Olive);
        FooterStyle = new Style(foreground: Color.White);
        ContentStyle = new Style(foreground: Color.White);
        SelectionStyle = new Style(foreground: Color.Gold3_1);
        SeparatorRow = 0;
    }
}