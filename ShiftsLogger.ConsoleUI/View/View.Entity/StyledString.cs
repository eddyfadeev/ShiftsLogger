using Spectre.Console;

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
    
    public Markup StyledText => 
        new(OriginString, AppliedStyle);
    
    public StyledString(string stringToStyle, Style? style = null)
    {
        OriginString = stringToStyle;
        AppliedStyle = style;
    }
    
    public StyledString() {}
    
    public static implicit operator StyledString(string originString) => 
        new(originString);
    
    public static implicit operator string(StyledString styledString) =>
        styledString.OriginString;
}