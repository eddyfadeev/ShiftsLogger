using Contracts.Repository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) {}

    public async Task<PagedList<User>> GetAllUsersAsync(UserParameters queryParameters, bool trackChanges)
    {
        var users =  await FindAll(trackChanges)
            .Search(queryParameters)
            .Sort(queryParameters)
            .Page(queryParameters)
            .ToListAsync();
        
        var count = await FindAll(trackChanges)
            .Search(queryParameters)
            .CountAsync();

        return new PagedList<User>
            (
                count, 
                queryParameters.PageNumber, 
                queryParameters.PageSize, 
                users
            );
    }

    public async Task<User?> GetUserByIdAsync(Guid userId, bool trackChanges) =>
        await FindByCondition(u => u.Id.Equals(userId), trackChanges)
            .SingleOrDefaultAsync();

    public void CreateUser(User user) =>
        Create(user);

    public void DeleteUser(User user) =>
        Delete(user);

    public bool UserExists(Guid userId) =>
        Exists(u => u.Id.Equals(userId));
}