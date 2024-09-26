using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Interfaces;

public interface IRenderService
{
    void RenderSinglePanelLayout(IRenderable panel);
    void RenderDoublePanelLayout(IRenderable leftPanel, IRenderable rightPanel);
}