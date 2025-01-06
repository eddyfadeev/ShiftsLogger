using Contracts;
using Services.Contracts;
using Spectre.Console;
using View.Contracts;
using View.Entity;

namespace Services;

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
            
            var menuToHandle = currentMenu.GetMenuView();
            ArgumentNullException.ThrowIfNull(menuToHandle, nameof(menuToHandle));
            
            Display(menuToHandle);
            
            await HandleKeyPressAsync(menuToHandle);
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

    private static void Display(object? menu)
    {
        if (menu is not IDisplayable displayable)
        {
            throw new ArgumentException(
                message: "Argument is not IDisplayable", 
                paramName: nameof(menu));
        }
        
        AnsiConsole.Clear();
        displayable.DisplayMenu();
    }
    
    private async Task HandleKeyPressAsync(ISelectable menu)
    {
        if (!Console.KeyAvailable)
        {
            await Task.Delay(100);
        }
        
        var keyPress = Console.ReadKey(true);

        switch (keyPress.Key)
        {
            case ConsoleKey.UpArrow:
                menu.SelectPrevious();
                break;
            case ConsoleKey.DownArrow:
                menu.SelectNext();
                break;
            case ConsoleKey.Enter:
                menu.InvokeAction();
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