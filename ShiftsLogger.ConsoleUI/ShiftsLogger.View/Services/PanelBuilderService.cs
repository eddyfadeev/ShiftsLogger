using System.Text;
using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.ViewModels;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Services;

public class PanelBuilderService : IPanelBuilderService
{
    public Panel PrepareRenderablePanel<TPanelEntries>(
        SinglePanelViewModel<TPanelEntries> renderInfo, HorizontalAlignment textAlignment
        )
        where TPanelEntries : class
    {
        Markup panelText = GetPanelText(
            entries: renderInfo.PanelEntries,
            color: "green",
            selectedIndex: renderInfo.SelectedEntryIndex
        );

        return CreatePanel(panelText, textAlignment);
    }
    
    public Tuple<Panel, Panel> PrepareRenderablePanels<TLeftPanelEntries, TRightPanelEntries>(
        DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> renderInfo,
        HorizontalAlignment leftPanelTextAlignment,
        HorizontalAlignment rightPanelTextAlignment)
        where TLeftPanelEntries : class
        where TRightPanelEntries : class
    {
        Markup leftPanelText = GetPanelText(
            entries: renderInfo.PanelEntries, 
            color: "green", 
            selectedIndex: renderInfo.SelectedEntryIndex, 
            isSinglePanel: renderInfo.IsSinglePanelMode
        );
        
        Markup rightPanelText;
        if (renderInfo.LastActiveSelectionIndex < 0 
            || !renderInfo.RightPanelEntries.TryGetValue(renderInfo.PanelEntries[renderInfo.LastActiveSelectionIndex], 
                out var shifts))
        {
            rightPanelText = new Markup("[grey]Choose a location to see available shifts...[/]");
        }
        else
        {
            rightPanelText = GetPanelText(
                entries: shifts, 
                color: "blue", 
                selectedIndex: renderInfo.RightPanelActiveIndex, 
                isSinglePanel: !renderInfo.IsSinglePanelMode
            );
        }
        
        var leftPanel = CreatePanel(leftPanelText, leftPanelTextAlignment);
        var rightPanel = CreatePanel(rightPanelText, rightPanelTextAlignment);

        return new Tuple<Panel, Panel>(leftPanel, rightPanel);
    }

    private static Panel CreatePanel(IRenderable textToDisplay, HorizontalAlignment textAlignment)
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
        List<TPanelEntry> entries, string color, int selectedIndex, bool isSinglePanel = true
    )
        where TPanelEntry : notnull
    {
        var sb = new StringBuilder();
        
        for (int i = 0; i < entries.Count; i++)
        {
            if (i == selectedIndex)
            {
                sb.Append((isSinglePanel ? $"[bold underline {color}]" : $"[{color}]") + entries[i] + "[/]\n");
            }
            else
            {
                sb.Append(entries[i] + "\n");
            }
        }

        return new Markup(sb.ToString());
    }
}