using Spectre.Console;
using View.Entity;

namespace View.Utility.Extensions;

public static class MenuEntryExtensions
{
    public static MenuEntry WithStyle(this MenuEntry menuEntry, Style? style) =>
        menuEntry with
        {
            EntryText = menuEntry.EntryText.ApplyStyling(style)
        };
}