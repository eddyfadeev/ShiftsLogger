using Spectre.Console;
using View.Entity;

namespace View.Utility.Extensions;

public static class StyledStringExtensions
{
    public static StyledString[] ApplyStyle(this StyledString[] strings, Style? textStyle, HorizontalAlignment alignment = HorizontalAlignment.Left) =>
        strings
            .Select(str => str.ApplyStyle(textStyle, alignment))
            .ToArray();

    public static StyledString ApplyStyle(this StyledString str, Style? textStyle, HorizontalAlignment alignment) =>
        new (str, textStyle, alignment);

    public static StyledString[] RemoveStyle(this StyledString[] strings, Style? style = null, HorizontalAlignment alignment = HorizontalAlignment.Left) =>
        strings
            .Select(str => str.RemoveStyle(style, alignment))
            .ToArray();

    public static StyledString RemoveStyle(this StyledString text, Style? style = null, HorizontalAlignment alignment = HorizontalAlignment.Left) =>
        new(text.OriginString, style, alignment);
}