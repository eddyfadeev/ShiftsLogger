using ShiftsLogger.Application.Interfaces.Data.Repositories;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Application.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public List<User> GetAllUsers() =>
        _usersRepository.GetAllUsers();

    public int AddUser(User user) =>
        _usersRepository.Add(user);

    public User? GetUser(int userId) =>
        _usersRepository.Get(userId);

    public int UpdateUser(User user) =>
        _usersRepository.Update(user);

    public int RemoveUser(User user) =>
        _usersRepository.Remove(user);
}