using Entities.Models.Entity;

namespace Contracts.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
    Task<User?> GetUserByIdAsync(Guid userId, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);
    void UpdateUser(User user);
    bool UserExists(Guid userId);
}