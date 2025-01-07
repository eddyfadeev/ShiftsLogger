using View.Contracts;
using View.Entity.Structures;
using View.Services.Contracts;

namespace View.Services;

public class MenuHandler : IMenuHandler
{
    private readonly Stack<IMenu> _menuStack = new();
    private bool _isRunning = true;
    
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
            await HandleKeyPressAsync(currentMenu);
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
    
    private async Task HandleKeyPressAsync(IMenu menu)
    {
        if (!Console.KeyAvailable)
        {
            await Task.Delay(100);
        }

        var selectable = menu.GetViewModel();

        var keyPress = Console.ReadKey(true);

        switch (keyPress.Key)
        {
            case ConsoleKey.UpArrow:
                selectable.SelectPrevious();
                break;
            case ConsoleKey.DownArrow:
                selectable.SelectNext();
                break;
            case ConsoleKey.Enter:
                selectable.InvokeAction();
                break;
            case ConsoleKey.Escape:
                PopMenu();
                break;
            case ConsoleKey.LeftArrow
                when menu is IPageable<EntityEntryData> pageable:
                // TODO: Fetch data and pass it
                pageable.PreviousPage();
                break;
            case ConsoleKey.RightArrow
                when menu is IPageable<EntityEntryData> pageable:
                // TODO: Fetch data and pass it
                pageable.NextPage();
                break;
        }
    }
}

public interface INavigationHandler
{
    Task HandleKeyPressAsync(ISelectable menu);
}