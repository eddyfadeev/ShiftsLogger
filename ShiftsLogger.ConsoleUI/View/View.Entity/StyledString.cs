using Spectre.Console;
using Spectre.Console.Rendering;

namespace View.Entity;

public readonly record struct StyledString
{
    private readonly string _originString;

    public string OriginString
    {
        get => _originString;
        init => _originString = string.IsNullOrWhiteSpace(value) 
            ? string.Empty 
            : value;
    }

    public Style? AppliedStyle { get; init; }
    public HorizontalAlignment Alignment { get; init; } = HorizontalAlignment.Left;

    public IRenderable StyledText => Alignment switch
    {
        HorizontalAlignment.Right => Align.Right(new Markup(OriginString, AppliedStyle)),
        HorizontalAlignment.Center => Align.Center(new Markup(OriginString, AppliedStyle)),
        _ => Align.Left(new Markup(OriginString, AppliedStyle))
    };
    
    public StyledString(string stringToStyle, Style? style = null, HorizontalAlignment alignment = HorizontalAlignment.Left)
    {
        OriginString = stringToStyle;
        AppliedStyle = style;
        Alignment = alignment;
    }
    
    public StyledString() {}
    
    public static implicit operator StyledString(string originString) => 
        new(originString);
    
    public static implicit operator string(StyledString styledString) =>
        styledString.OriginString;
}