using Shared.Dto.Location;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.LocationsMenu;

public sealed class ManageLocationsMenu : ManageEntitiesMenu<LocationDto>
{
    private const string Title = "Manage Locations";
    
    public ManageLocationsMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    private protected override async Task<PagedList<LocationDto>> 
        FetchEntities(RequestParameters? requestParameters = null) =>
        await ApiService.Location.GetAllLocationsAsync(requestParameters as LocationParameters);
}