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
    private const string DefaultFooter = 
        """
        Press ESC to return/exit
        Press up/down arrow keys to select
        Press Enter to confirm selection
        """;
    
    protected MenuView(IServiceProvider serviceProvider, string title, string? footer = null) 
        : base(serviceProvider, title, footer ?? DefaultFooter)
    {
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
    
    protected virtual Action CreateSubmenu<T>()
        where T : IMenu =>
        () =>
        {
            var submenu = ActivatorUtilities.CreateInstance<T>(ServiceProvider);
            MenuHandler.PushMenu(submenu);
        };
}