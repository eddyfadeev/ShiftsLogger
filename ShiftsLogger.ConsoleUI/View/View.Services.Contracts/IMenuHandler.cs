using View.Contracts;

namespace View.Services.Contracts;

public interface IMenuHandler
{
    Task RunAsync();
    void PushMenu(IMenu menu);
    void PopMenu();
}