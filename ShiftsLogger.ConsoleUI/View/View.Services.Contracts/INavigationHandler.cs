using View.Contracts;

namespace View.Services.Contracts;

public interface INavigationHandler
{
    Task HandleKeyPressAsync(ISelectable selectableMenu, IMenuHandler menuHandler);
}