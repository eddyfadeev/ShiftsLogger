using Spectre.Console;

namespace ShiftsLogger.View.Interfaces;

public interface IRenderService
{
    void Render(Layout layout);
}