using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Shared.Dto.Shift;
using Shared.RequestFeatures;
using Spectre.Console;
using View.Entity.Structures;
using View.Events;
using View.Services.Contracts;
using View.Utility;
using View.Utility.Extensions;
using View.View;

namespace ShiftsLogger.Presentation.ShiftsMenu;

public sealed class ManageShifts : EntitiesMenuView
{
    private const string Title = "Manage Shifts";
    private readonly IApiServiceManager _apiService;

    private PagedList<ShiftDto>? _shifts;
    public ManageShifts(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
        _apiService = serviceProvider.GetRequiredService<IApiServiceManager>();
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
        ArgumentNullException.ThrowIfNull(_shifts, nameof(_shifts));
        var shifts = _shifts.Select(shift =>
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
        _shifts = FetchShifts().GetAwaiter().GetResult();
    }

    private async Task<PagedList<ShiftDto>> FetchShifts(ShiftParameters? requestParameters = null)
    {
        var apiManager = ServiceProvider.GetRequiredService<IApiServiceManager>();
        return await apiManager.Shift.GetAllShiftsAsync(requestParameters);
    }

    protected override void HandlePageChanged(object sender, ChangePageEventArgs args)
    {
        if (!args.CurrentPage.HasValue)
        {
            return;
        }
        
        _shifts = FetchShifts(
            new ShiftParameters
            {
                PageNumber = args.CurrentPage.Value
            }
            ).GetAwaiter().GetResult();

        var substitutes = GetMenuEntries();
        MenuViewModel?.SubstituteData(substitutes);
    }
}



public class EditEntityMenuView<T> : MenuView
{
    public EditEntityMenuView(IServiceProvider serviceProvider, string title, string? footer = null) 
        : base(serviceProvider, title, footer)
    {
    }

    protected override IReadOnlyCollection<MenuEntry> GetMenuEntries()
    {
        throw new NotImplementedException();
    }
}