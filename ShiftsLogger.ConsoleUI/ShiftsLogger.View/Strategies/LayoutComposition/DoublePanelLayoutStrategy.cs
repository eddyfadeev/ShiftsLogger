using System.Collections.Immutable;
using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces.Strategies;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ShiftsLogger.View.Strategies.LayoutComposition;

public class DoublePanelLayoutStrategy : ILayoutStrategy
{
    private int _mainPanelRatio = 50; // console width percent
    private int _supportPanelRatio = 50; // console width percent

    public int MainPanelRatio
    {
        get => _mainPanelRatio;
        private set
        {
            if (value <= 0)
            {
                _mainPanelRatio = 50;
            }
            _mainPanelRatio = value;
        }
    }

    public int SupportPanelRatio
    {
        get => _supportPanelRatio;
        private set
        {
            if (value <= 0)
            {
                _supportPanelRatio = 50;
            }
            _supportPanelRatio = value;
        }
    }

    public LayoutSplit SplitDirection { get; private set; } = LayoutSplit.Vertical;

    public DoublePanelLayoutStrategy(int? splitRatio = null, LayoutSplit? splitDirection = null)
    {
        if (splitRatio is not null)
        {
            SetRatio(splitRatio.Value);
        }

        if (splitDirection is not null)
        {
            SplitDirection = splitDirection.Value;
        }
    }
    
    public Layout Compose(params IEnumerable<IRenderable> renderables)
    {
        var objects = renderables.ToImmutableList();
        
        if (objects.Count <= 0)
        {
            throw new ArgumentException("No renderables were passed to compose a layout");
        }

        if (objects.Count > 2)
        {
            throw new ArgumentException("No more than two renderables allowed");
        }

        var layout = SplitDirection switch
        {
            LayoutSplit.Vertical =>
                new Layout().SplitColumns(
                    new Layout("left", objects[0]).Ratio(MainPanelRatio),
                    new Layout("right", objects[1]).Ratio(SupportPanelRatio)
                ),
            LayoutSplit.Horizontal =>
                new Layout().SplitRows(
                    new Layout("top", objects[0]).Ratio(MainPanelRatio),
                    new Layout("bottom", objects[1]).Ratio(SupportPanelRatio)
                ),
            _ => throw new ArgumentException("Invalid split type")
        };

        return layout;
    }

    /// <summary>
    /// Sets the main panel ratio and automatically calculates
    /// the secondary panel's ratio and assigns it.
    /// </summary>
    /// <param name="ratio">MainPanelRatio</param>
    /// <exception cref="ArgumentException">Valid ratio range: 0 - 100</exception>
    /// <remarks>SupportPanelRatio = maxRatio - MainPanelRatio</remarks>
    public void SetRatio(int ratio)
    {
        const int maxRatio = 100;
        if (ratio < 0)
        {
            throw new ArgumentException("Ratio should be non-negative");
        }

        if (ratio > 100)
        {
            throw new ArgumentException("Ratio cannot be more than 100");
        }
        
        MainPanelRatio = ratio;
        SupportPanelRatio = maxRatio - MainPanelRatio;
    }

    public void SetSplitDirection(LayoutSplit splitDirection)
    {
        SplitDirection = splitDirection;
    }
}