using Spectre.Console;
using View.Entity.Structures;
using View.Utility.Extensions;

namespace View.Entity.Extensions;

public static class MenuEntryExtensions
{
    public static MenuEntry WithStyle(this MenuEntry menuEntry, Style? style) =>
        menuEntry with
        {
            EntryText = menuEntry.EntryText.ApplyStyling(style)
        };
}