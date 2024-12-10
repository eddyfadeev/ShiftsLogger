using Entities.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;

public class ShiftTypeRepositoryTests
{
    private RepositoryContext _context;
    private ShiftTypeRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        _context = await ImMemoryDbProvider.CreateDatabaseContext();
        _repository = new ShiftTypeRepository(_context);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }

    [Test]
    public async Task GetAllShiftTypesAsync_ReturnsShiftTypesOrderedByName()
    {
        var result = await _repository.GetAllShiftTypesAsync(trackChanges: false);
        
        Assert.That(result, Is.Ordered.By(nameof(ShiftType.Name)));
    }

    [Test]
    public async Task GetAllShiftTypesAsync_ReturnsCorrectShiftTypes()
    {
        var expected = await _context.ShiftTypes.ToListAsync();

        var result = await _repository.GetAllShiftTypesAsync(trackChanges: false);

        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetAllShiftTypesAsync_ReturnsEmptyEnumerable_WhenNoShiftTypesInDb()
    {
        await _context.Database.EnsureDeletedAsync();

        var result = await _repository.GetAllShiftTypesAsync(trackChanges: false);

        Assert.That(result, Is.Empty);
    }
    
    
    [Test]
    public async Task GetShiftTypeByIdAsync_ReturnsCorrectShiftType()
    {
        var expected = await _context.ShiftTypes.FirstAsync();

        var result = await _repository.GetShiftTypeByIdAsync(expected.Id, trackChanges: false);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task GetShiftTypeByIdAsync_ReturnsNull_WhenNoMatchesInDb()
    {
        var randomId = Guid.NewGuid();
        
        var result = await _repository.GetShiftTypeByIdAsync(randomId, trackChanges: false);
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void CreateShiftType_CreatesEntityWithStateAdded()
    {
        var expected = new ShiftType
        {
            Id = Guid.NewGuid(),
            Name = "Test Name"
        };

        _repository.CreateShiftType(expected);

        var result = _context.ChangeTracker
            .Entries<ShiftType>()
            .First(e => e.State == EntityState.Added);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }

    [Test]
    public void CreateShiftType_ThrowsNullReferenceException_WhenPassedNull()
    {
        Assert.Throws<NullReferenceException>(() =>
            _repository.CreateShiftType(null));
    }
    
    [Test]
    public void DeleteShiftType_CreatesEntityWithStateDeleted()
    {
        var expected = new ShiftType
        {
            Id = Guid.NewGuid(),
            Name = "Test Name"
        };

        _repository.DeleteShiftType(expected);

        var result = _context.ChangeTracker
            .Entries<ShiftType>()
            .First(e => e.State == EntityState.Deleted);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteShiftType_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteShiftType(null));
    }
    
    [Test]
    public void UpdateShiftType_CreatesEntityWithStateModified()
    {
        var expected = new ShiftType
        {
            Id = Guid.NewGuid(),
            Name = "Test Name"
        };

        _repository.UpdateShiftType(expected);

        var result = _context.ChangeTracker
            .Entries<ShiftType>()
            .First(e => e.State == EntityState.Modified);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpdateShiftType_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteShiftType(null));
    }

    [Test]
    public async Task ShiftTypeExists_ReturnsTrueWhenShiftTypeExists()
    {
        var testShiftType = await _context.ShiftTypes.FirstAsync();

        var result = _repository.ShiftTypeExists(testShiftType.Id);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ShiftTypeExists_ReturnsFalseWhenShiftTypeDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        
        var result = _repository.ShiftTypeExists(randomId);
        
        Assert.That(result, Is.False);
    }
}