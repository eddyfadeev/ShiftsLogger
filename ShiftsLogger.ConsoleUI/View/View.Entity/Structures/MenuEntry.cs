namespace View.Entity.Structures;

public readonly record struct MenuEntry
{
    public StyledString Name { get; init; }
    public Action? Action { get; init; }

    public MenuEntry(string name, Action? entryAction = null, StringStyling? entryStyle = null)
    {
        Name = new StyledString(name, entryStyle);
        Action = entryAction;
    }

    public MenuEntry(StyledString name, Action? entryAction = null)
    {
        Name = name; 
        Action = entryAction;
    }
    
    public MenuEntry() {}
}