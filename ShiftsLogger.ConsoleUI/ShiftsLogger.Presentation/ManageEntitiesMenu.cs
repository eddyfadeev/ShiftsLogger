using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Shared.RequestFeatures;
using Spectre.Console;
using View.Entity.Structures;
using View.Events;
using View.Services.Contracts;
using View.Utility;
using View.Utility.Extensions;
using View.View;

namespace ShiftsLogger.Presentation;

public abstract class ManageEntitiesMenu<T> : EntitiesMenuView
{
    private protected IApiServiceManager? ApiService;

    private protected PagedList<T>? Entities;
    
    public ManageEntitiesMenu(IServiceProvider serviceProvider, string title, string? footer = null) 
        : base(serviceProvider, title, footer)
    {
        
    }

    public override void DisplayMenu()
    {
        ArgumentNullException.ThrowIfNull(BuilderStrategy, nameof(BuilderStrategy));
        ArgumentNullException.ThrowIfNull(MenuViewModel, nameof(MenuViewModel));
        
        var tableBuilder = ServiceProvider.GetRequiredService<ITableBuilder>();
        var table = tableBuilder.Build(MenuViewModel.MenuData, BuilderStrategy);

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    protected override IReadOnlyCollection<EntityEntryData> GetMenuEntries()
    {
        ArgumentNullException.ThrowIfNull(Entities, nameof(Entities));
        var shifts = Entities.Select(shift =>
        {
            var entryHeaders =
                ClassDataExtractor
                    .GetPropertyNames(shift, "id")
                    .Select(header =>
                        new Text(header.SplitCamelCase(), Settings?.Styles.TableHeader)
                    ).ToArray();
            
            var entryData =
                ClassDataExtractor
                    .GetPropertyValuesAsString(shift, "id")
                    .Select(entry =>
                        new Text(entry, Settings?.Styles.Content)
                            .Overflow(Overflow.Fold)
                    ).ToArray();

            return new EntityEntryData(entryHeaders, entryData);
        }).ToList();
        
        return new ReadOnlyCollection<EntityEntryData>(shifts);
    }

    protected override void OnMenuCreation()
    {
        ApiService = ServiceProvider.GetRequiredService<IApiServiceManager>();
        Entities = FetchEntities().GetAwaiter().GetResult();
    }

    private protected abstract Task<PagedList<T>> FetchEntities(RequestParameters? requestParameters = null);

    protected override void HandlePageChanged(object sender, ChangePageEventArgs args)
    {
        if (!args.CurrentPage.HasValue)
        {
            return;
        }
        
        Entities = FetchEntities(
            new ShiftParameters
            {
                PageNumber = args.CurrentPage.Value
            }
            ).GetAwaiter().GetResult();

        var substitutes = GetMenuEntries();
        MenuViewModel?.SubstituteData(substitutes);
    }
}