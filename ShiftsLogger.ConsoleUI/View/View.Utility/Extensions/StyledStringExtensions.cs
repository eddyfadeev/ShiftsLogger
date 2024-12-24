using Spectre.Console;
using View.Entity;

namespace View.Utility.Extensions;

public static class StyledStringExtensions
{
    public static StyledString[] ApplyStyle(this StyledString[] strings, Style? textStyle) =>
        strings
            .Select(str => str.ApplyStyle(textStyle))
            .ToArray();

    public static StyledString ApplyStyle(this StyledString str, Style? textStyle) =>
        new (str, textStyle);

    public static StyledString[] RemoveStyle(this StyledString[] strings, Style? style = null) =>
        strings
            .Select(str => str.RemoveStyle(style))
            .ToArray();

    public static StyledString RemoveStyle(this StyledString text, Style? style = null) =>
        new(text.OriginString, style);
}