namespace View.Entity.Models;

public sealed class MenuSettings : TableSettings
{
    public MenuSettings()
    {
        ShowHeaders = false;
        ShowFooter = true;
        Expand = true;
        SeparatorRow = 0;
    }
}