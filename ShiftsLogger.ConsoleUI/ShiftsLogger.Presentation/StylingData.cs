using Spectre.Console;
using View.Entity.Structures;

namespace ShiftsLogger.Presentation;

internal static class StylingData
{
    internal static readonly StringStyling DefaultTitleStyling = new()
    {
        AppliedStyle = new Style(foreground: Color.Wheat1),
        HorizontalAlignment = HorizontalAlignment.Center
    };
    
    internal static readonly StringStyling DefaultMenuEntryStyling = new()
    {
        AppliedStyle = new Style(foreground: Color.White),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    internal static readonly StringStyling DefaultHeaderStyling = new()
    {
        AppliedStyle = new Style(foreground: Color.Wheat1),
        HorizontalAlignment = HorizontalAlignment.Center
    };

    internal static readonly StringStyling DefaultSelectionStyling = new()
    {
        AppliedStyle = new Style(foreground: Color.Gold3_1),
        HorizontalAlignment = HorizontalAlignment.Center
    };
}