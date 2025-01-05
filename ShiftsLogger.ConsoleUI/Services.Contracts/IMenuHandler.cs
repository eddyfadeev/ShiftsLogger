using View.Contracts;

namespace Services.Contracts;

public interface IMenuHandler
{
    Task RunAsync();
    void PushMenu(ISelectable menu);
    void PopMenu();
}