using Shared.Dto.User;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IUserService
{
    Task<(List<UserDto> users, PaginationMetaData metaData)>
        GetAllUsersAsync(UserParameters userParameters, bool trackChanges);
    Task<UserDto> GetUserByIdAsync(Guid userId, bool trackChanges);
    Task<UserDto> CreateUserAsync(UserForCreationDto user);
    Task DeleteUserAsync(Guid userId, bool trackChanges);
    Task UpdateUserAsync(Guid userId, UserForUpdateDto updateDto, bool trackChanges);
}