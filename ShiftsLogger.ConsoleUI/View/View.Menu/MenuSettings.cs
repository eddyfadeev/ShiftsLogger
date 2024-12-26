using Spectre.Console;
using View.Entity;
using View.Entity.Structures;

namespace View.Menu;

public sealed class MenuSettings : TableSettings
{
    public MenuSettings()
    {
        ShowColumnHeaders = false;
        ShowFooter = true;
        Expand = true;
        BorderStyle = TableBorder.Rounded;
        TitleStyle = new StringStyling(Color.Olive, HorizontalAlignment.Center);
        HeadersStyle = null;
        ContentStyle = new StringStyling(Color.White, HorizontalAlignment.Center);
        SelectionStyle = new StringStyling(Color.Gold3_1, HorizontalAlignment.Center);
        SeparatorRow = 0;
    }
}