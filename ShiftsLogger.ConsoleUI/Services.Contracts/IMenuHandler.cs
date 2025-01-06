using Contracts;

namespace Services.Contracts;

public interface IMenuHandler
{
    Task RunAsync();
    void PushMenu(IMenu menu);
    void PopMenu();
}