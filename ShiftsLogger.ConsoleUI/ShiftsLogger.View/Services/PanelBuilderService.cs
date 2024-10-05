using System.Text;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;

namespace ShiftsLogger.View.Services;

public class PanelBuilderService : IPanelBuilderService
{
    public Panel CreatePanel(
        ISinglePanelViewModel viewModel, 
        HorizontalAlignment textAlignment, 
        Color color,
        bool isSinglePanel = true,
        params TextDecorations[] textDecorations
        )
    {
        Markup panelText = GetPanelText(
            entries: viewModel.PanelEntries.VisibleElements,
            selectedIndex: viewModel.CurrentIndex,
            color: color,
            textDecorations: textDecorations,
            isSinglePanel: isSinglePanel
            );
        
        var panel = textAlignment switch
        {
            HorizontalAlignment.Center => new Panel(Align.Center(panelText)),
            HorizontalAlignment.Right => new Panel(Align.Right(panelText)),
            HorizontalAlignment.Left => new Panel(Align.Left(panelText)),
            _ => new Panel(Align.Left(panelText))
        };
        
        ApplyDefaultConfiguration(panel);

        return panel;
    }

    public Panel CreateDummyPanel(string text)
    {
        var dummyPanel = new Panel(text);
        ApplyDefaultConfiguration(dummyPanel);

        return dummyPanel;
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