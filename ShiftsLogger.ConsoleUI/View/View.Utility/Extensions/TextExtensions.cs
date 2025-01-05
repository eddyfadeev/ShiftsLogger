using Spectre.Console;

namespace View.Utility.Extensions;

public static class TextExtensions
{
    public static Text[] ApplyStyling(this IEnumerable<Text> texts, Style? style) => 
        texts.Select(text => text.ApplyStyling(style)).ToArray();

    public static Text ApplyStyling(this Text text, Style? style) =>
        new(text.GetString(), style);
    
    public static string GetString(this Text text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        
        return string.Join(
            "", text
                .GetSegments(AnsiConsole.Console)
                .Select(segment =>
                    segment.Text)
        );
    }
}