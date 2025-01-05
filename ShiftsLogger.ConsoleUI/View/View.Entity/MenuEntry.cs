using Spectre.Console;
using View.Contracts;

namespace View.Entity;

public readonly record struct MenuEntry : IActionable
{
    public MenuEntry(string entryText, Action? entryAction = null, Style? entryStyle = null)
    {
        EntryText = new Text(entryText, entryStyle);
        Action = entryAction;
    }
    
    public Text EntryText { get; init; }
    public Action? Action { get; init; }
}