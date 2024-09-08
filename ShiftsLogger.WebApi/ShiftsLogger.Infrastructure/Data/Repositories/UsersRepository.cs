using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ShiftsLoggerDbContext _context;

    public UsersRepository(ShiftsLoggerDbContext context)
    {
        _context = context;
    }
    public int Add(User entity)
    {
        _context.Users.Add(entity);
        return _context.SaveChanges();
    }

    public User? Get(int entityId) => 
        _context.Users.FirstOrDefault(u => u.Id == entityId);

    public int Update(User entity)
    {
        _context.Users.Update(entity);
        return _context.SaveChanges();
    }

    public int Remove(User entity)
    {
        _context.Users.Remove(entity);
        return _context.SaveChanges();
    }

    public List<User> GetAllUsers() => 
        _context.Users.ToList();
}