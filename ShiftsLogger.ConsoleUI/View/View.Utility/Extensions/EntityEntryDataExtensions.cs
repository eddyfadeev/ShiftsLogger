using Spectre.Console;
using View.Entity;

namespace View.Utility.Extensions;

public static class EntityEntryDataExtensions
{
    public static EntityEntryData WithStyle(this EntityEntryData entryData, Style? style) =>
        entryData with
        {
            EntryData = entryData.EntryData.ApplyStyling(style)
        };
}