using Spectre.Console;

namespace View.Entity.Structures;

public readonly record struct StringStyling
{
    public Style? AppliedStyle { get; init; }
    public HorizontalAlignment HorizontalAlignment { get; init; } = HorizontalAlignment.Left;
    public VerticalAlignment? VerticalAlignment { get; init; }

    public StringStyling(
        Style? style = null,
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment? verticalAlignment = null)
    {
        AppliedStyle = style;
        HorizontalAlignment = horizontalAlignment;
        VerticalAlignment = verticalAlignment;
    }
}