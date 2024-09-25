using System.Text;
using ShiftsLogger.Domain.Interfaces;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.ConsoleApp.ConsoleUI;

public class RenderService
{
    private const string FiltersList = "Filters";
    private const string ShiftsList = "Shifts";
    private const int FiltersRatio = 30; // Percentage of console's width 
    private const int ShiftsRatio = 70;  // Percentage of console's width

    private readonly Layout _menuLayout;

    public RenderService()
    {
        _menuLayout = InitializeMenuLayout();
    }

    public void RenderLayout<TEntity>(ShiftsView<TEntity> renderInfo) 
        where TEntity: class, IReportModel
    {
        
        string filtersPanelText = GetPanelText(renderInfo.Entities, "green" , renderInfo.SelectedEntityIndex, renderInfo.IsLeftPaneActive);
        
        string shiftsPanelText;
        if (renderInfo.LastActiveSelectionIndex < 0 || !renderInfo.FilteredShifts.TryGetValue(renderInfo.Entities[renderInfo.LastActiveSelectionIndex], out var shifts))
        {
            shiftsPanelText = "[grey]Choose a location to see available shifts...[/]";
        }
        else
        {
            shiftsPanelText = GetPanelText(shifts, "blue", renderInfo.SelectedShiftIndex, !renderInfo.IsLeftPaneActive);
        }
        
        var leftPanel = DrawPanel(filtersPanelText.TrimEnd());
        var rightPanel = DrawPanel(shiftsPanelText.TrimEnd());

        UpdateLayout(leftPanel, rightPanel);
    }

    private void UpdateLayout(IRenderable leftPanel, IRenderable rightPanel)
    {
        _menuLayout[FiltersList].Update(leftPanel);
        _menuLayout[ShiftsList].Update(rightPanel);
        
        AnsiConsole.Clear();
        AnsiConsole.Write(_menuLayout);
    }
    
    private static Layout InitializeMenuLayout() =>
        new Layout().SplitColumns(
            new Layout(FiltersList).Ratio(FiltersRatio),
            new Layout(ShiftsList).Ratio(ShiftsRatio)
        );

    private static Panel DrawPanel(string textToDisplay) =>
        new(textToDisplay)
        {
            Expand = true,
            Border = BoxBorder.Rounded
        };
    
    private static string GetPanelText<TEntity>(List<TEntity> entities, string color, int selectedIndex, bool isLeftPaneActive)
        where TEntity: class, IReportModel
    {
        var sb = new StringBuilder();
        
        for (int i = 0; i < entities.Count; i++)
        {
            if (i == selectedIndex)
            {
                sb.Append((isLeftPaneActive ? $"[bold underline {color}]" : $"[{color}]") + entities[i] + "[/]\n");
            }
            else
            {
                sb.Append(entities[i] + "\n");
            }
        }

        return sb.ToString();
    }
}
