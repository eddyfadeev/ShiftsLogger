namespace ShiftsLogger.View.Interfaces.ViewModels.SinglePanel;

public interface IReturnsEntry<out T>
{
    T GetCurrentElement();
}