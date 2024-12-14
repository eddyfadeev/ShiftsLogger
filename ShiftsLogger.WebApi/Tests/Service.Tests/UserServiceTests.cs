using Contracts;
using Contracts.Repository;
using Entities.Exceptions.NotFound;
using Entities.Models;
using NSubstitute;
using Shared.Dto.User;
using Shared.RequestFeatures;

namespace Service.Tests;

public class UserServiceTests
{
    private IUserRepository _userRepositoryMock;
    private IRepositoryManager _repositoryManagerMock;
    private ServiceManager _serviceManager;
    
    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _repositoryManagerMock = Substitute.For<IRepositoryManager>();
        
        _repositoryManagerMock.User.Returns(_userRepositoryMock);
        
        _repositoryManagerMock.SaveAsync()
            .Returns(Task.CompletedTask);

        _serviceManager = new ServiceManager(_repositoryManagerMock);
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsListOfUsers()
    {
        var testUsers = new PagedList<User>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "1",
                Email = "user1@example.com",
                Role = "Admin"
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "2",
                Email = "user2@example.com",
                Role = "Admin"
            }
        );
        
        var expected = testUsers.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u!.FirstName,
            LastName = u!.LastName,
            Email = u!.Email,
            Role = u!.Role
        }).ToList();
        
        _repositoryManagerMock.User
            .GetAllUsersAsync(Arg.Any<UserParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testUsers));

        var result =
            await _serviceManager.UserService.GetAllUsersAsync(new UserParameters(), trackChanges: false);
        
        Assert.That(result.users, Is.EquivalentTo(expected));
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsPaginationMetaData()
    {
        var expected = new PagedList<User>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "1",
                Email = "user1@example.com",
                Role = "Admin"
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "2",
                Email = "user2@example.com",
                Role = "Admin"
            }
        );

        _repositoryManagerMock.User
            .GetAllUsersAsync(Arg.Any<UserParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(expected));

        var result =
            await _serviceManager.UserService.GetAllUsersAsync(new UserParameters(), false);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.metaData.CurrentPage, Is.EqualTo(expected.PaginationMetaData.CurrentPage));
            Assert.That(result.metaData.PageSize, Is.EqualTo(expected.PaginationMetaData.PageSize));
            Assert.That(result.metaData.TotalCount, Is.EqualTo(expected.PaginationMetaData.TotalCount));
            Assert.That(result.metaData.TotalPages, Is.EqualTo(expected.PaginationMetaData.TotalPages));
        });
    }

    [Test]
    public async Task GetUserByIdAsync_ReturnsUserDto_WhenUserExists()
    {
        var id = Guid.NewGuid();

        var testUser = new User
        {
            Id = id,
            FirstName = "User",
            LastName = "1",
            Email = "user1@example.com",
            Role = "Admin"
        };
        
        var expected = new UserDto
        {
            Id = id,
            FirstName = testUser.FirstName,
            LastName = testUser.LastName,
            Email = testUser.Email,
            Role = testUser.Role
        };
        
        _userRepositoryMock.UserExists(Arg.Any<Guid>())
            .Returns(true);
        _userRepositoryMock.GetUserByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testUser));

        var result = await _serviceManager.UserService.GetUserByIdAsync(id, false);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetUserByIdAsync_ThrowsNotFoundException_WhenUserDoesNotExist() =>
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await _serviceManager.UserService.GetUserByIdAsync(Guid.NewGuid(), false));

    [Test]
    public async Task CreateUserAsync_ReturnsUserDto_WhenCreatedNew()
    {
        var user = new UserForCreationDto
        {
            FirstName = "User", 
            LastName = "1", 
            Email = "user@example.com", 
            Role = "Admin"
        };
        
        var expected = new UserDto
        {
            Id = Guid.NewGuid(), 
            FirstName = user.FirstName, 
            LastName = user.LastName, 
            Email = user.Email, 
            Role = user.Role
        };   
        
        _repositoryManagerMock.User.CreateUser(Arg.Any<User>());
        
        var result = await _serviceManager.UserService.CreateUserAsync(user);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(expected.FirstName));
            Assert.That(result.LastName, Is.EqualTo(expected.LastName));
            Assert.That(result.Email, Is.EqualTo(expected.Email));
            Assert.That(result.Role, Is.EqualTo(expected.Role));
        }); 
    }

    [Test]
    public void DeleteUserAsync_ThrowsUserNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await _serviceManager.UserService.DeleteUserAsync(Guid.NewGuid(), false));

    [Test]
    public async Task DeleteUserAsync_DeletesExistingUser()
    {
        var id = Guid.NewGuid();

        var testUser = new User 
        {
            Id = id,
            FirstName = "User", 
            LastName = "1", 
            Email = "user@example.com", 
            Role = "Admin"
        };
        
        _userRepositoryMock.UserExists(Arg.Any<Guid>())
            .Returns(true);
        _userRepositoryMock.GetUserByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testUser));
        
        await _serviceManager.UserService.DeleteUserAsync(id, false);

        _repositoryManagerMock.ReceivedWithAnyArgs().User.DeleteUser(testUser);
        Console.WriteLine();
    }
    
    [Test]
    public void UpdateUserAsync_ThrowsUserNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
        {
            await _serviceManager.UserService.UpdateUserAsync(Guid.NewGuid(), new UserForUpdateDto(), false);
        });

    [Test]
    public async Task UpdateUserAsync_DeletesExistingUser()
    {
        var id = Guid.NewGuid();
        
        var testUser = new User 
        {
            Id = id,
            FirstName = "User", 
            LastName = "1", 
            Email = "user@example.com", 
            Role = "Admin"
        };
        
        _userRepositoryMock.UserExists(Arg.Any<Guid>())
            .Returns(true);
        _userRepositoryMock.GetUserByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testUser));
        
        await _serviceManager.UserService.UpdateUserAsync(Guid.NewGuid(), new UserForUpdateDto(), false);
        
        await _repositoryManagerMock.ReceivedWithAnyArgs().SaveAsync();
    }
}