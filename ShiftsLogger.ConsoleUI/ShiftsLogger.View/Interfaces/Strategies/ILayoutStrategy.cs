using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Interfaces.Strategies;

public interface ILayoutStrategy
{
    Layout Compose(params IEnumerable<IRenderable> renderables);
}