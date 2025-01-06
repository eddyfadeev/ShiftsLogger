using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using View.Contracts;
using View.Entity;

namespace ShiftsLogger.Presentation;

public abstract class MenuViewBase<TMenu, TMenuEntry> : IMenu
    where TMenu : ISelectable, IDisplayable
{
    private TMenu? _menuView;
    
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMenuHandler MenuHandler;

    private protected TableSettings? Settings { get; private set; }
    private protected IReadOnlyCollection<TMenuEntry>? MenuEntries { get; private set; }

    protected MenuViewBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        MenuHandler = serviceProvider.GetRequiredService<IMenuHandler>();
        
        BuildViewModel();
    }
    
    public ISelectable GetMenuView() =>
        _menuView ?? 
        throw new ArgumentNullException
        (
            paramName: nameof(_menuView), 
            message: "Menu view is null."
        );
    
    protected abstract TMenu CreateMenu();
    protected abstract IReadOnlyCollection<TMenuEntry> GetMenuEntries();
    protected abstract TableSettings CreateSettings();
    
    protected virtual void OnMenuCreation()
    {
        // Any logic that needs to be executed when the menu is created.
    }
    
    private void BuildViewModel()
    {
        OnMenuCreation();
        Settings = CreateSettings();
        MenuEntries = GetMenuEntries();
        _menuView = CreateMenu();
    }
}