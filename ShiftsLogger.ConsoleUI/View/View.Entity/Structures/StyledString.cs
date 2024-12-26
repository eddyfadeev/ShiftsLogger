using Spectre.Console;
using Spectre.Console.Rendering;

namespace View.Entity.Structures;

public readonly record struct StyledString
{
    public StyledString(
        string stringToStyle, 
        StringStyling? styling = null)
    {
        OriginString = string.IsNullOrWhiteSpace(stringToStyle)
            ? string.Empty
            : stringToStyle;

        Styling = styling ?? new StringStyling();
    }

    public StyledString()
    {
        OriginString = string.Empty;
        Styling = new StringStyling();
    }

    public string OriginString { get; } = string.Empty;

    public StringStyling Styling { get; } = new();

    public IRenderable StyledText => Styling.HorizontalAlignment switch
    {
        HorizontalAlignment.Right => 
            Align.Right(
                new Markup(OriginString, Styling.AppliedStyle),
                Styling.VerticalAlignment),
        HorizontalAlignment.Center => 
            Align.Center(
                new Markup(OriginString, Styling.AppliedStyle),
                Styling.VerticalAlignment),
        _ => Align.Left(
            new Markup(OriginString, Styling.AppliedStyle),
            Styling.VerticalAlignment)
    };

    public static implicit operator StyledString(string originString) => 
        new(originString);

    public static implicit operator string(StyledString styledString) =>
        styledString.OriginString;
}