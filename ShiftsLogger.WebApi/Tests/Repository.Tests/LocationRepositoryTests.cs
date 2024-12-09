using Entities.Models.Entity;

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
    public async Task GetAllLocations_ReturnsOrderedLocations()
    {
        var result = await _repository.GetAllLocationsAsync(trackChanges: false);
        
        Assert.That(result, Is.Ordered.By(nameof(Location.Name)));
    }
}