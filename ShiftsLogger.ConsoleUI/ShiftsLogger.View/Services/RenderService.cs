using ShiftsLogger.View.Interfaces;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Services;

public class RenderService : IRenderService
{
    private readonly Layout _menuLayout = new ();

    public void RenderDoublePanelLayout(IRenderable leftPanel, IRenderable rightPanel)
    {
        const string leftPanelName = "leftPanel";
        const string rightPanelName = "rightPanel";
        const int leftPanelWidth = 30; // Percentage of console's width 
        const int rightPanelWidth = 70;  // Percentage of console's width
        
        try
        {
            _menuLayout.SplitColumns(
                new Layout(leftPanelName).Ratio(leftPanelWidth),
                new Layout(rightPanelName).Ratio(rightPanelWidth));
        }
        catch (InvalidOperationException)
        {
            // Do nothing, exception will be thrown if
            // You will try to split, already divided layout
        }

        _menuLayout[leftPanelName].Update(leftPanel);
        _menuLayout[rightPanelName].Update(rightPanel);
        
        UpdateConsole(_menuLayout);
    }

    public void RenderSinglePanelLayout(IRenderable panel)
    {
        _menuLayout.Update(panel);
        
        UpdateConsole(_menuLayout);
    }
    
    private static void UpdateConsole(Layout menuLayout)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(menuLayout);
    }
}
