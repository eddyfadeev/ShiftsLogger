using System.Collections.Immutable;
using ShiftsLogger.View.Interfaces.Strategies;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Strategies.LayoutComposition;

public sealed class SinglePanelLayoutStrategy : ILayoutStrategy
{
    public Layout Compose(params IEnumerable<IRenderable> renderables)
    {
        var objects = renderables.ToImmutableList();

        return objects.Count switch
        {
            <= 0 => throw new ArgumentException("No renderables were passed to compose a layout"),
            > 1 => throw new ArgumentException("Just one renderable in single panel layout is allowed"),
            _ => new Layout(objects[0])
        };
    }
}