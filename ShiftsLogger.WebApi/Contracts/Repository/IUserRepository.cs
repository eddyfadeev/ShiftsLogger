using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Repository;

public interface IUserRepository
{
    Task<PagedList<User>> GetAllUsersAsync(UserParameters queryParameters, bool trackChanges);
    Task<User?> GetUserByIdAsync(Guid userId, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
    bool UserExists(Guid userId);
}