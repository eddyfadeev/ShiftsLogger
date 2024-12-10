using Entities.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;

public class ShiftRepositoryTests
{
    private RepositoryContext _context;
    private ShiftRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        _context = await ImMemoryDbProvider.CreateDatabaseContext();
        _repository = new ShiftRepository(_context);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }

    [Test]
    public async Task GetAllShiftsAsync_ReturnsShiftsOrderedByName()
    {
        var result = await _repository.GetAllShiftsAsync(trackChanges: false);
        
        Assert.That(result, Is.Ordered.By(nameof(Shift.StartTime)));
    }

    [Test]
    public async Task GetAllShiftsAsync_ReturnsCorrectShifts()
    {
        var expected = await _context.Shifts.ToListAsync();

        var result = await _repository.GetAllShiftsAsync(trackChanges: false);

        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetAllShiftsAsync_ReturnsEmptyEnumerable_WhenNoShiftsInDb()
    {
        await _context.Database.EnsureDeletedAsync();

        var result = await _repository.GetAllShiftsAsync(trackChanges: false);

        Assert.That(result, Is.Empty);
    }
    
    
    [Test]
    public async Task GetShiftByIdAsync_ReturnsCorrectShift()
    {
        var expected = await _context.Shifts.FirstAsync();

        var result = await _repository.GetShiftByIdAsync(expected.Id, trackChanges: false);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task GetShiftByIdAsync_ReturnsNull_WhenNoMatchesInDb()
    {
        var randomId = Guid.NewGuid();
        
        var result = await _repository.GetShiftByIdAsync(randomId, trackChanges: false);
        
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public void CreateShift_CreatesEntityWithStateAdded()
    {
        var expected = new Shift
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-12),
            EndTime = DateTime.Now.AddHours(-4),
            Description = "Test"
        };

        _repository.CreateShift(expected);

        var result = _context.ChangeTracker
            .Entries<Shift>()
            .First(e => e.State == EntityState.Added);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }

    [Test]
    public void CreateShift_ThrowsNullReferenceException_WhenPassedNull()
    {
        Assert.Throws<NullReferenceException>(() =>
            _repository.CreateShift(null));
    }
    
    [Test]
    public void DeleteShift_CreatesEntityWithStateDeleted()
    {
        var expected = new Shift
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-12),
            EndTime = DateTime.Now.AddHours(-4),
            Description = "Test"
        };

        _repository.DeleteShift(expected);

        var result = _context.ChangeTracker
            .Entries<Shift>()
            .First(e => e.State == EntityState.Deleted);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteShift_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteShift(null));
    }
    
    [Test]
    public void UpdateShift_CreatesEntityWithStateModified()
    {
        var expected = new Shift
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-12),
            EndTime = DateTime.Now.AddHours(-4),
            Description = "Test"
        };

        _repository.UpdateShift(expected);

        var result = _context.ChangeTracker
            .Entries<Shift>()
            .First(e => e.State == EntityState.Modified);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpdateShift_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteShift(null));
    }

    [Test]
    public async Task ShiftExists_ReturnsTrueWhenShiftExists()
    {
        var testShift = await _context.Shifts.FirstAsync();

        var result = _repository.ShiftExists(testShift.Id);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ShiftExists_ReturnsFalseWhenShiftDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        
        var result = _repository.ShiftExists(randomId);
        
        Assert.That(result, Is.False);
    }
}