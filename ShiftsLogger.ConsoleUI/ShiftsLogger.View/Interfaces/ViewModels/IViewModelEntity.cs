namespace ShiftsLogger.View.Interfaces.ViewModels;

public interface IViewModelEntity
{
    int ElementHeight { get; }
    void SetElementHeight(int height);
    string GetShortRepresentation();
    string GetDetailedRepresentation();
}