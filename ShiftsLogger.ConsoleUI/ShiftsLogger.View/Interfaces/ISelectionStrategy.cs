using ShiftsLogger.View.Enums;

namespace ShiftsLogger.View.Interfaces;

public interface ISelectionStrategy
{
    void ChangeSelection(Selection move);
}