using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Interfaces.Services;

public interface ILayoutComposerService
{
    Layout GetLayout();
    void SetRenderables(params IEnumerable<IRenderable> renderables);
}