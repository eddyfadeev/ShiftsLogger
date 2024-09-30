using ShiftsLogger.View.Enums;
using ShiftsLogger.View.Interfaces;
using ShiftsLogger.View.Interfaces.Services;

namespace ShiftsLogger.View.Services;

public class SelectionService : ISelectionService
{
    private ISelectionStrategy _selectionStrategy;

    public SelectionService(ISelectionStrategy selectionStrategy)
    {
        _selectionStrategy = selectionStrategy;
    }

    public void ChangeSelection(Selection move)
    {
        _selectionStrategy?.ChangeSelection(move);
    }
}