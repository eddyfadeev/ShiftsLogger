using Microsoft.Extensions.DependencyInjection;
using View.Entity.Models;
using View.Entity.Structures;
using View.Entity.ViewModels;
using View.Services;
using View.Services.Contracts;

namespace View.View;

public abstract class EntitiesMenuView : MenuViewBase<EntitiesMenuViewModel, EntityEntryData>
{
    private const string DefaultFooter = 
        """
        Press ESC to return/exit
        Press up/down arrow keys to select
        Press left/right arrow keys to change a page
        Press Enter to confirm selection
        """;
    
    protected EntitiesMenuView(IServiceProvider serviceProvider, string title, string? footer = null) 
        : base(serviceProvider, title, footer ?? DefaultFooter)
    {
    }

    protected override TableSettings CreateSettings() =>
        new EntitiesMenuSettings();

    protected override EntitiesMenuViewModel CreateMenu()
    {
        var tableBuilder = ServiceProvider.GetRequiredService<ITableBuilder>();
        
        ArgumentNullException.ThrowIfNull(tableBuilder, nameof(tableBuilder));
        ArgumentNullException.ThrowIfNull(Settings, nameof(Settings));
        ArgumentNullException.ThrowIfNull(MenuEntries, nameof(MenuEntries));
        
        var menuView = new EntitiesMenuViewModel((EntitiesMenuSettings)Settings, MenuEntries);

        return menuView;
    }

    protected override ITableBuilderStrategy<EntityEntryData> CreateStrategy() =>
        new EntitiesMenuBuilderStrategy();
}