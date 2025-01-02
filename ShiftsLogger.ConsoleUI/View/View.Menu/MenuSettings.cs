using Spectre.Console;
using View.Entity;

namespace View.Menu;

public sealed class MenuSettings : RenderSettings
{
    public MenuSettings()
    {
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