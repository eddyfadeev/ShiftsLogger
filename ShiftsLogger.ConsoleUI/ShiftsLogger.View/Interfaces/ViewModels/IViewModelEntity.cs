namespace ShiftsLogger.View.Interfaces.ViewModels;

public interface IViewModelEntity
{
    int ElementHeight => CalculateElementHeight();

    private int CalculateElementHeight()
    {
        return 0;
    }
}