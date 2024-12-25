namespace View.Entity;

public class MenuEntry
{
    public string Name { get; set; }
    public Action? Action { get; set; }

    public MenuEntry(string name, Action? action = null)
    {
        Name = name;
        Action = action;
    }
}