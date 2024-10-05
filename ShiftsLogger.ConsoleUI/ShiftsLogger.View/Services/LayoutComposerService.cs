using System.Collections.Immutable;
using ShiftsLogger.View.Interfaces.Services;
using ShiftsLogger.View.Interfaces.Strategies;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Services;

public class LayoutComposerService : ILayoutComposerService
{
    private readonly ILayoutStrategy _layoutStrategy;
    private ImmutableList<IRenderable> _renderables;

    public LayoutComposerService(ILayoutStrategy layoutStrategy, params IEnumerable<IRenderable> renderables)
    {
        _layoutStrategy = layoutStrategy;
        _renderables = renderables.ToImmutableList();
    }

    public Layout GetLayout() =>
        _layoutStrategy.Compose(_renderables);

    public void SetRenderables(params IEnumerable<IRenderable> renderables) => 
        _renderables = renderables.ToImmutableList();
}