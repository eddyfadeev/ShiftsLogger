using Shared.Dto.User;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.UsersMenu;

public sealed class ManageUsersMenu : ManageEntitiesMenu<UserDto>
{
    private const string Title = "Manage Users";
    public ManageUsersMenu(IServiceProvider serviceProvider) 
        : base(serviceProvider, Title)
    {
    }

    private protected override async Task<PagedList<UserDto>>
        FetchEntities(RequestParameters? requestParameters = null) =>
        await ApiService.User.GetAllUsersAsync(requestParameters as UserParameters);
}