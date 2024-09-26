using System.Text;
using ShiftsLogger.ConsoleApp.ConsoleUI.ViewModels;
using ShiftsLogger.View.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.View.Services;

public class PanelBuilderService : IPanelBuilderService
{
    public Panel PrepareRenderablePanel<TPanelEntries>(SinglePanelViewModel<TPanelEntries> renderInfo)
        where TPanelEntries : class
    {
        string panelText = GetPanelText(
            entries: renderInfo.PanelEntries,
            color: "green",
            selectedIndex: renderInfo.SelectedEntryIndex
        );

        var panel = CreatePanel(panelText.TrimEnd());

        return panel;
    }
    
    public Tuple<Panel, Panel> PrepareRenderablePanels<TLeftPanelEntries, TRightPanelEntries>(DoublePanelViewModel<TLeftPanelEntries, TRightPanelEntries> renderInfo)
        where TLeftPanelEntries : class
        where TRightPanelEntries : class
    {
        string filtersPanelText = GetPanelText(
            entries: renderInfo.PanelEntries, 
            color: "green", 
            selectedIndex: renderInfo.SelectedEntryIndex, 
            isSinglePanel: renderInfo.IsSinglePanelMode
        );
        
        string shiftsPanelText;
        if (renderInfo.LastActiveSelectionIndex < 0 
            || !renderInfo.RightPanelEntries.TryGetValue(renderInfo.PanelEntries[renderInfo.LastActiveSelectionIndex], 
                out var shifts))
        {
            shiftsPanelText = "[grey]Choose a location to see available shifts...[/]";
        }
        else
        {
            shiftsPanelText = GetPanelText(
                entries: shifts, 
                color: "blue", 
                selectedIndex: renderInfo.RightPanelActiveIndex, 
                isSinglePanel: !renderInfo.IsSinglePanelMode
            );
        }
        
        var leftPanel = CreatePanel(filtersPanelText.TrimEnd());
        var rightPanel = CreatePanel(shiftsPanelText.TrimEnd());

        return new Tuple<Panel, Panel>(leftPanel, rightPanel);
    }

    private static Panel CreatePanel(string textToDisplay) =>
        new(textToDisplay)
        {
            Expand = true,
            Border = BoxBorder.Rounded
        };
    
    private static string GetPanelText<TPanelEntry>(
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

        return sb.ToString();
    }
}