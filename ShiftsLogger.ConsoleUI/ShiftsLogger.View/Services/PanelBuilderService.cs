using System.Text;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Services;

public class PanelBuilderService : IPanelBuilderService
{
    public Panel CreatePanel<TPanelEntries>(
        SinglePanelViewModel<TPanelEntries> renderInfo, 
        HorizontalAlignment textAlignment, 
        Color color,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
        )
        where TPanelEntries : class
    {
        Markup panelText = GetPanelText(
            entries: renderInfo.PanelEntries.VisibleElements,
            selectedIndex: renderInfo.CurrentIndex,
            color: color,
            textDecorations: textDecorations,
            isSinglePanel: isSinglePanel
            );
        
        return AlignText(panelText, textAlignment);
    }

    public Panel CreateDummyPanel(string text)
    {
        var dummyPanel = new Panel(text);
        ApplyDefaultConfiguration(dummyPanel);

        return dummyPanel;
    }

    private static Panel AlignText(Markup textToDisplay, HorizontalAlignment textAlignment)
    {
        var panel = textAlignment switch
        {
            HorizontalAlignment.Center => new Panel(Align.Center(textToDisplay)),
            HorizontalAlignment.Right => new Panel(Align.Right(textToDisplay)),
            HorizontalAlignment.Left => new Panel(Align.Left(textToDisplay)),
            _ => new Panel(Align.Left(textToDisplay))
        };
        
        ApplyDefaultConfiguration(panel);

        return panel;
    }

    private static void ApplyDefaultConfiguration(Panel panel)
    {
        panel.Expand = true;
        panel.Border = BoxBorder.Rounded;
    }
    
    private static Markup GetPanelText<TPanelEntry>(
        IEnumerable<TPanelEntry> entries, 
        Color color, 
        int selectedIndex, 
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
    )
        where TPanelEntry : notnull
    {
        var sb = new StringBuilder();
        string decorations = GetTextDecorations(textDecorations);
        string textColor = color.ToString().ToLower();
        
        for (int i = 0; i < entries.Count(); i++)
        {
            if (i == selectedIndex)
            {
                sb.Append((isSinglePanel ? $"[{decorations} {textColor}]" : $"[{textColor}]") + entries.ElementAt(i) + "[/]\n");
            }
            else
            {
                sb.Append(entries.ElementAt(i) + "\n");
            }
        }

        return new Markup(sb.ToString());
    }

    private static string GetTextDecorations(params TextDecorations[] textDecorations)
        => string.Join(" ", textDecorations.Select(d => d.ToString().ToLower()));
}