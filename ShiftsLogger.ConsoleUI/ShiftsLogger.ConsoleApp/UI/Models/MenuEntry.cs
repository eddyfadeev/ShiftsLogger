using ShiftsLogger.View.ViewModels;

namespace ShiftsLogger.ConsoleApp.UI.Models;

public class MenuEntry : ViewModelEntity
{
    private readonly string _name;
    
    public MenuEntry(string name, int elementHeight)
    {
        SetElementHeight(elementHeight);
        _name = name;
    }

    public override string ToString() => _name;
}