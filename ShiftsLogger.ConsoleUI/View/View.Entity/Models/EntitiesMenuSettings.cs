namespace View.Entity.Models;

public sealed class EntitiesMenuSettings : TableSettings
{
    public EntitiesMenuSettings()
    {
        ShowHeaders = true;
        ShowFooter = true;
        Expand = false; 
        SeparatorRow = 5;
    }
}