using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using View.Contracts;
using View.Entity.Models;
using View.Entity.ViewModels;
using View.Services.Contracts;

namespace View.View;

public abstract class MenuViewBase<TMenu, TMenuEntry> : IMenu
    where TMenu : MenuViewModelBase<TMenuEntry>
    where TMenuEntry : IActionable
{
    private readonly string _title;
    private readonly string _footer;
    
    protected TMenu? MenuViewModel;
    
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMenuHandler MenuHandler;

    protected TableSettings? Settings;
    protected IReadOnlyCollection<TMenuEntry>? MenuEntries;
    protected ITableBuilderStrategy<TMenuEntry>? BuilderStrategy;

    protected MenuViewBase(IServiceProvider serviceProvider, string title, string footer)
    {
        _title = title;
        _footer = footer;
        
        ServiceProvider = serviceProvider;
        MenuHandler = serviceProvider.GetRequiredService<IMenuHandler>();
        
        BuildViewModel();
    }

    public ISelectable GetViewModel() =>
        MenuViewModel ?? 
        throw new ArgumentNullException
        (
            paramName: nameof(MenuViewModel), 
            message: "Menu view is null."
        );

    public virtual void DisplayMenu()
    {
        ArgumentNullException.ThrowIfNull(BuilderStrategy, nameof(BuilderStrategy));
        ArgumentNullException.ThrowIfNull(MenuViewModel, nameof(MenuViewModel));
        
        var tableBuilder = ServiceProvider.GetRequiredService<ITableBuilder>();
        var table = tableBuilder.Build(MenuViewModel.MenuData, BuilderStrategy);
        
        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }
    
    protected abstract TableSettings CreateSettings();
    protected abstract IReadOnlyCollection<TMenuEntry> GetMenuEntries();
    protected abstract TMenu CreateMenu();
    protected abstract ITableBuilderStrategy<TMenuEntry> CreateStrategy();
    
    protected virtual void OnMenuCreation()
    {
        // Any logic that needs to be executed before the menu is created.
    }

    protected virtual void OnMenuCreated()
    {
        // Any logic that needs to be executed after the menu is created.
    }
    
    private void BuildViewModel()
    {
        OnMenuCreation();
        Settings = CreateSettings();
        MenuEntries = GetMenuEntries();
        MenuViewModel = CreateMenu();
        BuilderStrategy = CreateStrategy();
        
        MenuViewModel.SetTitle(_title);
        MenuViewModel.SetFooter(_footer);
        
        OnMenuCreated();
    }
}