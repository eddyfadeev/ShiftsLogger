namespace ShiftsLogger.ConsoleApp.ConsoleUI;

public interface IRenderService
{
    Task RenderLayout(int selectedLocationIndex, int selectedShiftIndex, bool isLeftPaneActive, bool locationConfirmed, int previousLocationIndex);
}