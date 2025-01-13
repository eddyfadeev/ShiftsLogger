using Shared.Dto.Shift;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.ShiftsMenu;

public sealed class ManageShiftsMenu : ManageEntitiesMenu<ShiftDto>
{
    private const string Title = "Manage Shifts";
    
    public ManageShiftsMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    private protected override async Task<PagedList<ShiftDto>> 
        FetchEntities(RequestParameters? requestParameters = null) => 
        await ApiService.Shift.GetAllShiftsAsync(requestParameters as ShiftParameters);
            
}