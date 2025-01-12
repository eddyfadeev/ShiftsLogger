using View.Contracts;
using View.Services.Contracts;

namespace View.Services;

public class MenuHandler : IMenuHandler
{
    private readonly INavigationHandler _navigationHandler;
    
    private readonly Stack<IMenu> _menuStack = new();
    private bool _isRunning = true;

    public MenuHandler(INavigationHandler navigationHandler) =>
        _navigationHandler = navigationHandler;
    
    public async Task RunAsync()
    {
        while (_isRunning)
        {
            if (!_menuStack.TryPeek(out var currentMenu))
            {
                _isRunning = false;
                return;
            }
            
            currentMenu.DisplayMenu();
            await _navigationHandler.HandleKeyPressAsync(selectableMenu: currentMenu.GetViewModel(), menuHandler: this);
        }
    }

    public void PushMenu(IMenu menu) =>
        _menuStack.Push(menu);

    public void PopMenu()
    {
        if (_menuStack.Count > 1)
        {
            _menuStack.Pop();
        }
        else
        {
            _isRunning = false;
        }
    }
}