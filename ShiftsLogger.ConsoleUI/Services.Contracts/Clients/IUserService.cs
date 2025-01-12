using Shared.Dto.User;
using Shared.RequestFeatures;

namespace Services.Contracts.Clients;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<PagedList<UserDto>> GetAllUsersAsync(UserParameters? requestParameters = null);
    Task<UserDto?> CreateUserAsync(UserDto user);
    Task DeleteUserAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, UserDto user);
}