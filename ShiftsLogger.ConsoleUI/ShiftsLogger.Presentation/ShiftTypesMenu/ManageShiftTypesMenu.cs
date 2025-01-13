using Shared.Dto.ShiftType;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.ShiftTypesMenu;

public sealed class ManageShiftTypesMenu : ManageEntitiesMenu<ShiftTypeDto>
{
    private const string Title = "Manage Shift Types";
    
    public ManageShiftTypesMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    private protected override async Task<PagedList<ShiftTypeDto>>
        FetchEntities(RequestParameters? requestParameters = null) =>
        await ApiService.ShiftType.GetAllShiftTypesAsync(requestParameters as ShiftTypeParameters);
}