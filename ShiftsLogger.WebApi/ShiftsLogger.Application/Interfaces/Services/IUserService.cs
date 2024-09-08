using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Interfaces.Services;

public interface IUserService
{
    List<User> GetAllUsers();
    int AddUser(User user);
    User? GetUser(int userId);
    int UpdateUser(User user);
    int RemoveUser(User user);
}