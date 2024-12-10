using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(u => u.FirstName)
            .ToListAsync();

    public async Task<User?> GetUserByIdAsync(Guid userId, bool trackChanges) =>
        await FindByCondition(u => u.Id.Equals(userId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateUser(User user) =>
        Create(user);

    public void DeleteUser(User user) =>
        Delete(user);

    public void UpdateUser(User user) =>
        Update(user);

    public bool UserExists(Guid userId) =>
        Exists(u => u.Id.Equals(userId));
}