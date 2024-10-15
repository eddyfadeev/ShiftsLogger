using ShiftsLogger.View.Enums;

namespace ShiftsLogger.View.Interfaces.Services;

public interface ISelectionService
{
    void ChangeSelection(Selection move);
}