using Microsoft.Extensions.DependencyInjection;
using View.Contracts;
using View.Entity.Models;
using View.Entity.Structures;
using View.Entity.ViewModels;
using View.Services;
using View.Services.Contracts;

namespace View.View;

public abstract class MenuView : MenuViewBase<MenuViewModel, MenuEntry>
{
    private readonly string _title;
    private readonly string _footer;
    
    protected MenuView(IServiceProvider serviceProvider, string title, string footer) 
        : base(serviceProvider)
    {
        _title = title;
        _footer = footer;
    }
    
    protected override MenuViewModel CreateMenu()
    {
        var tableBuilder = ServiceProvider.GetRequiredService<ITableBuilder>();
        
        ArgumentNullException.ThrowIfNull(tableBuilder, nameof(tableBuilder));
        ArgumentNullException.ThrowIfNull(Settings, nameof(Settings));
        ArgumentNullException.ThrowIfNull(MenuEntries, nameof(MenuEntries));
        
        return new MenuViewModel((MenuSettings)Settings, MenuEntries);
    }
    
    protected override TableSettings CreateSettings() =>
        new MenuSettings();
    
    protected override ITableBuilderStrategy<MenuEntry> CreateStrategy() =>
        new MenuBuilderStrategy();
    
    protected override void OnMenuCreated()
    {
        MenuViewModel?.SetTitle(_title);
        MenuViewModel?.SetFooter(_footer);
    }
    
    protected virtual Action CreateSubmenu<T>()
        where T : IMenu =>
        () =>
        {
            var submenu = ActivatorUtilities.CreateInstance<T>(ServiceProvider);
            MenuHandler.PushMenu(submenu);
        };
}