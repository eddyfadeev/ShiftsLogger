using Entities.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;

public class LocationRepositoryTests
{
    private RepositoryContext _context;
    private LocationRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        _context = await ImMemoryDbProvider.CreateDatabaseContext();
        _repository = new LocationRepository(_context);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }

    [Test]
    public async Task GetAllLocationsAsync_ReturnsLocationsOrderedByName()
    {
        var result = await _repository.GetAllLocationsAsync(trackChanges: false);
        
        Assert.That(result, Is.Ordered.By(nameof(Location.Name)));
    }

    [Test]
    public async Task GetAllLocationsAsync_ReturnsCorrectLocations()
    {
        var expected = await _context.Locations.ToListAsync();

        var result = await _repository.GetAllLocationsAsync(trackChanges: false);

        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetAllLocationsAsync_ReturnsEmptyEnumerable_WhenNoLocationsInDb()
    {
        await _context.Database.EnsureDeletedAsync();

        var result = await _repository.GetAllLocationsAsync(trackChanges: false);

        Assert.That(result, Is.Empty);
    }
    
    
    [Test]
    public async Task GetLocationByIdAsync_ReturnsCorrectLocation()
    {
        var expected = await _context.Locations.FirstAsync();

        var result = await _repository.GetLocationByIdAsync(expected.Id, trackChanges: false);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public async Task GetLocationByIdAsync_ReturnsNull_WhenNoMatchesInDb()
    {
        var randomId = Guid.NewGuid();
        
        var result = await _repository.GetLocationByIdAsync(randomId, trackChanges: false);
        
        Assert.That(result, Is.Null);
    }

    [Test]
    public void CreateLocation_CreatesEntityWithStateAdded()
    {
        var expected = new Location
        {
            Id = Guid.NewGuid(),
            Name = "Test Location",
            Address = "Test address"
        };

        _repository.CreateLocation(expected);

        var result = _context.ChangeTracker
            .Entries<Location>()
            .First(e => e.State == EntityState.Added);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }

    [Test]
    public void CreateLocation_ThrowsNullReferenceException_WhenPassedNull()
    {
        Assert.Throws<NullReferenceException>(() =>
            _repository.CreateLocation(null));
    }
    
    [Test]
    public void DeleteLocation_CreatesEntityWithStateDeleted()
    {
        var expected = new Location
        {
            Id = Guid.NewGuid(),
            Name = "Test Location",
            Address = "Test address"
        };

        _repository.DeleteLocation(expected);

        var result = _context.ChangeTracker
            .Entries<Location>()
            .First(e => e.State == EntityState.Deleted);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void DeleteLocation_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteLocation(null));
    }
    
    [Test]
    public void UpdateLocation_CreatesEntityWithStateModified()
    {
        var expected = new Location
        {
            Id = Guid.NewGuid(),
            Name = "Test Location",
            Address = "Test address"
        };

        _repository.UpdateLocation(expected);

        var result = _context.ChangeTracker
            .Entries<Location>()
            .First(e => e.State == EntityState.Modified);
        
        Assert.That(result.Entity, Is.EqualTo(expected));
    }
    
    [Test]
    public void UpdateLocation_ThrowsArgumentNullException_WhenPassedNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _repository.DeleteLocation(null));
    }

    [Test]
    public async Task LocationExists_ReturnsTrueWhenLocationExists()
    {
        var testLocation = await _context.Locations.FirstAsync();

        var result = _repository.LocationExists(testLocation.Id);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void LocationExists_ReturnsFalseWhenLocationDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        
        var result = _repository.LocationExists(randomId);
        
        Assert.That(result, Is.False);
    }
}