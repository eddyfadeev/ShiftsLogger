using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class MenuEntry : ViewModelEntity
{
    private readonly string _name;
    
    public MenuEntry(string name, int elementHeight)
    {
        ElementHeight = elementHeight;
        _name = name;
    }

    public override string ToString() => _name;
    public override string GetShortRepresentation() => _name;
    public override string GetDetailedRepresentation() => _name;
}