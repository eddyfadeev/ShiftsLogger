using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces.Services;
using ShiftsLogger.View.Interfaces.Strategies;

namespace ShiftsLogger.View.Services;

public class SelectionService : ISelectionService
{
    private readonly ISelectionStrategy _selectionStrategy;

    public SelectionService(ISelectionStrategy selectionStrategy)
    {
        _selectionStrategy = selectionStrategy;
    }

    public void ChangeSelection(Selection move)
    {
        _selectionStrategy.ChangeSelection(move);
    }
}