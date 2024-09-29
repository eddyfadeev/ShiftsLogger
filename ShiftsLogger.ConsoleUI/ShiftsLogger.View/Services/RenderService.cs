using ShiftsLogger.View.Interfaces;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Services;

public class RenderService : IRenderService
{
    private Layout _menuLayout = new ();
    private bool _isDoublePanel;
    

    public void RenderDoublePanelLayout(IRenderable leftPanel, IRenderable rightPanel)
    {
        const string leftPanelName = "leftPanel";
        const string rightPanelName = "rightPanel";
        const int leftPanelWidth = 30; // Percentage of console's width 
        const int rightPanelWidth = 70;  // Percentage of console's width
        
        if (!_isDoublePanel)
        {
            _menuLayout.SplitColumns(
                new Layout(leftPanelName).Ratio(leftPanelWidth),
                new Layout(rightPanelName).Ratio(rightPanelWidth));
        }

        _menuLayout[leftPanelName].Update(leftPanel);
        _menuLayout[rightPanelName].Update(rightPanel);
        
        UpdateConsole(_menuLayout);
        _isDoublePanel = true;
    }

    public void RenderSinglePanelLayout(IRenderable panel)
    {
        if (_isDoublePanel)
        {
            _menuLayout = new Layout();
            _isDoublePanel = false;
        }
        
        _menuLayout.Update(panel);
        
        UpdateConsole(_menuLayout);
    }
    
    private static void UpdateConsole(Layout menuLayout)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(menuLayout);
    }
}
