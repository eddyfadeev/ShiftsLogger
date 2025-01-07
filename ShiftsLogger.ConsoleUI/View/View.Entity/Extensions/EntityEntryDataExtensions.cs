using Spectre.Console;
using View.Entity.Structures;
using View.Utility.Extensions;

namespace View.Entity.Extensions;

public static class EntityEntryDataExtensions
{
    public static EntityEntryData WithStyle(this EntityEntryData entryData, Style? style) =>
        entryData with
        {
            EntryData = entryData.EntryData.ApplyStyling(style)
        };
}