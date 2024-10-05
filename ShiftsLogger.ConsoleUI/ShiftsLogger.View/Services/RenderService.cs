using ShiftsLogger.View.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.View.Services;

public class RenderService : IRenderService
{
    public void Render(Layout layout)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(layout);
    }
}
