using View.Entity.Structures;

namespace View.Utility.Extensions;

public static class StyledStringExtensions
{
    public static StyledString[] ApplyStyle(this StyledString[] strings, StringStyling? textStyle) =>
        strings
            .Select(str => str.ApplyStyle(textStyle))
            .ToArray();

    public static StyledString ApplyStyle(this StyledString str, StringStyling? textStyle) =>
        new (str, textStyle);

    public static StyledString[] RemoveStyle(this StyledString[] strings, StringStyling? style = null) =>
        strings
            .Select(str => str.RemoveStyle(style))
            .ToArray();

    public static StyledString RemoveStyle(this StyledString text, StringStyling? style = null) =>
        new(text.OriginString, style);
}