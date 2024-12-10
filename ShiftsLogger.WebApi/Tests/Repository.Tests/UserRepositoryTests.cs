using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;

public class UserRepositoryTests
{
    private RepositoryContext _context;
    private UserRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        _context = await ImMemoryDbProvider.CreateDatabaseContext();
        _repository = new UserRepository(_context);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsUsersOrderedByFirstName()
    {
        var result = await _repository.GetAllUsersAsync(trackChanges: false);
        
        Assert.That(result, Is.Ordered.By(nameof(User.FirstName)));
    }

    [Test]
    public async Task GetAllUsersAsync_ReturnsCorrectUsers()
    {
        var expected = await _context.Users.ToListAsync();

        var result = await _repository.GetAllUsersAsync(trackChanges: false);

        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetAllUsersAsync_ReturnsEmptyEnumerable_WhenNoUsersInDb()
    {
        await _context.Database.EnsureDeletedAsync();

        var result = await _repository.GetAllUsersAsync(trackChanges: false);

        Assert.That(result, Is.Empty);
    }
    
    
    [Test]
    public async Task GetUserByIdAsync_ReturnsCorrectUser()
    {
        var expected = await _context.Users.FirstAsync();

        var result = await _repository.GetUserByIdAsync(expected.Id, trackChanges: false);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task GetUserByIdAsync_ReturnsNull_WhenNoMatchesInDb()
    {
        var randomId = Guid.NewGuid();
        
        var result = await _repository.GetUserByIdAsync(randomId, trackChanges: false);
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void CreateUser_CreatesEntityWithStateAdded()
    {
        var expected = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test First Name",
            LastName = "Test Last Name",
            Email = "Test Email",
            Role = "Test Role"
        };

        _repository.CreateUser(expected);

        var result = _context.ChangeTracker
            .Entries<User>()
            .First(e => e.State == EntityState.Added);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }

    [Test]
    public void CreateUser_ThrowsNullReferenceException_WhenPassedNull()
    {
        Assert.Throws<NullReferenceException>(() =>
            _repository.CreateUser(null));
    }
    
    [Test]
    public void DeleteUser_CreatesEntityWithStateDeleted()
    {
        var expected = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test First Name",
            LastName = "Test Last Name",
            Email = "Test Email",
            Role = "Test Role"
        };

        _repository.DeleteUser(expected);

        var result = _context.ChangeTracker
            .Entries<User>()
            .First(e => e.State == EntityState.Deleted);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteUser_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteUser(null));
    }
    
    [Test]
    public void UpdateUser_CreatesEntityWithStateModified()
    {
        var expected = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test First Name",
            LastName = "Test Last Name",
            Email = "Test Email",
            Role = "Test Role"
        };

        _repository.UpdateUser(expected);

        var result = _context.ChangeTracker
            .Entries<User>()
            .First(e => e.State == EntityState.Modified);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpdateUser_ThrowsNullReferenceException_WhenPassedNull()
    {
        Assert.Throws<NullReferenceException>(() =>
            _repository.UpdateUser(null));
    }

    [Test]
    public async Task UserExists_ReturnsTrueWhenUserExists()
    {
        var testUser = await _context.Users.FirstAsync();

        var result = _repository.UserExists(testUser.Id);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void UserExists_ReturnsFalseWhenUserDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        
        var result = _repository.UserExists(randomId);
        
        Assert.That(result, Is.False);
    }
}