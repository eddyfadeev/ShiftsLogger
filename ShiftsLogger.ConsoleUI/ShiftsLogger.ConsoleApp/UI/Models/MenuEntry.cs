using ShiftsLogger.View.Interfaces.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class MenuEntry : IViewModelEntity
{
    private readonly string _name;

    public int ElementHeight { get; }
    
    public MenuEntry(string name, int elementHeight)
    {
        ElementHeight = elementHeight;
        _name = name;
    }

    public override string ToString() => _name;
}