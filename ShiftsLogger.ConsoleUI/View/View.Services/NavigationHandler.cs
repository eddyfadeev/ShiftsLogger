using View.Contracts;
using View.Services.Contracts;

namespace View.Services;

public class NavigationHandler : INavigationHandler
{
    public async Task HandleKeyPressAsync(ISelectable selectableMenu, IMenuHandler menuHandler)
    {
        if (!Console.KeyAvailable)
        {
            await Task.Delay(100);
        }

        var keyPress = Console.ReadKey(true);

        if (keyPress.Key == ConsoleKey.Escape)
        {
            menuHandler.PopMenu();
            return;
        }

        HandleVerticalNavigation(selectableMenu, keyPress.Key);

        if (selectableMenu is IPageable pageable)
        {
            HandleHorizontalNavigation(pageable, keyPress.Key);
        }
    }

    private static void HandleVerticalNavigation(ISelectable menu, ConsoleKey keyPress)
    {
        switch (keyPress)
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
            default:
                return;
        }
    }

    private static void HandleHorizontalNavigation(IPageable menu, ConsoleKey keyPress)
    {
        switch (keyPress)
        {
            case ConsoleKey.LeftArrow:
                menu.PreviousPage();
                break;
            case ConsoleKey.RightArrow:
                menu.NextPage();
                break;
            default:
                return;
        }
    }
}