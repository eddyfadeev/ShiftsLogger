using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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
    public async Task GetAllShiftsAsync_ReturnsShiftsOrderedByStartTimeByDefault()
    {
        var expected = await _context.Shifts.OrderByDescending(s => s.StartTime).FirstAsync();
        var result = await _repository.GetAllShiftsAsync(new ShiftParameters(), trackChanges: false);
        
        Assert.That(result[0], Is.EqualTo(expected));
    }
    
    [Test]
    public async Task GetAllShiftsAsync_ReturnsEmptyEnumerable_WhenNoShiftsInDb()
    {
        await _context.Database.EnsureDeletedAsync();

        var result = await _repository.GetAllShiftsAsync(new ShiftParameters(), trackChanges: false);

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
    public async Task GetByIdsAsync_ReturnsCorrectShifts()
    {
        var expected = await _context.Shifts.AsNoTracking().Take(5).ToListAsync();
        var testIds = expected.Select(s => s.Id).ToList();
        
        var result = await _repository.GetByIdsAsync(testIds, trackChanges: false);
        
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetByIdsAsync_ReturnsEmptyEnumerable_WhenNoMatchesInDb()
    {
        List<Guid> testIds = [Guid.NewGuid(), Guid.NewGuid()];
        var result = await _repository.GetByIdsAsync(testIds, trackChanges: false);
        
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task GetByIdsAsync_ReturnsEmptyEnumerable_WhenPassedEmptyEnumerable()
    {
        var result = await _repository.GetByIdsAsync([], trackChanges: false);
        
        Assert.That(result, Is.Empty);
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

    [Test]
    public async Task GetShiftsForUser_ReturnsEmptyEnumerable_WhenNoMatchInDb()
    {
        var randomId = Guid.NewGuid();

        var result = await _repository.GetShiftsForUser(randomId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task GetShiftsForUser_ReturnsUsers()
    {
        var testId = (await _context.Users.FirstAsync()).Id;
        var expected = await _context.Shifts
            .Where(s => s.UserId.Equals(testId))
            .ToListAsync();

        var result = await _repository.GetShiftsForUser(testId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetShiftsForShiftType_ReturnsEmptyEnumerable_WhenNoMatchInDb()
    {
        var randomId = Guid.NewGuid();

        var result = await _repository.GetShiftsForShiftType(randomId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task GetShiftsForShiftType_ReturnsShiftTypes()
    {
        var testId = (await _context.ShiftTypes.FirstAsync()).Id;
        var expected = await _context.Shifts
            .Where(s => s.ShiftTypeId.Equals(testId))
            .ToListAsync();

        var result = await _repository.GetShiftsForShiftType(testId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetShiftsForLocation_ReturnsEmptyEnumerable_WhenNoMatchInDb()
    {
        var randomId = Guid.NewGuid();

        var result = await _repository.GetShiftsForLocation(randomId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task GetShiftsForLocation_ReturnsShiftTypes()
    {
        var testId = (await _context.Locations.FirstAsync()).Id;
        var expected = await _context.Shifts
            .Where(s => s.LocationId.Equals(testId))
            .ToListAsync();

        var result = await _repository.GetShiftsForLocation(testId, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result, Is.EquivalentTo(expected));
    }
}