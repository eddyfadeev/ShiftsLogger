using Contracts;
using Contracts.Repository;
using Entities.Exceptions.NotFound;
using Entities.Models;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shared.Dto.Location;
using Shared.RequestFeatures;

namespace Service.Tests;

public class Tests
{
    private ILocationRepository _locationRepositoryMock;
    private IRepositoryManager _repositoryManagerMock;
    private ServiceManager _serviceManager;

    private PagedList<Location> _locations;
    
    [SetUp]
    public void Setup()
    {
        _locationRepositoryMock = Substitute.For<ILocationRepository>();
        _repositoryManagerMock = Substitute.For<IRepositoryManager>();
        _repositoryManagerMock.Location.Returns(_locationRepositoryMock);

        _serviceManager = new ServiceManager(_repositoryManagerMock);
        _locations = new PagedList<Location>
        (
            count: 4,
            pageNumber: 1,
            pageSize: 4, 
            new Location { Id = Guid.NewGuid(), Name = "Location 1", Address = "Address 1" }, 
            new Location { Id = Guid.NewGuid(), Name = "Location 2", Address = "Address 2" }, 
            new Location { Id = Guid.NewGuid(), Name = "Location 3", Address = "Address 3" }, 
            new Location { Id = Guid.NewGuid(), Name = "Location 4", Address = "Address 4" }
        );
    }

    [Test]
    public async Task GetAllLocationsAsync_ReturnsListOfLocations()
    {
        var expected = _locations.Select(l => new LocationDto
        {
            Id = l.Id,
            Address = l!.Address,
            Name = l.Name
        }).ToList();
        
        _repositoryManagerMock.Location
            .GetAllLocationsAsync(Arg.Any<LocationParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(_locations));

        var result =
            await _serviceManager.LocationService.GetAllLocationsAsync(new LocationParameters(), trackChanges: false);
        
        Assert.That(result.locations, Is.EquivalentTo(expected));
    }

    [Test]
    public async Task GetAllLocationsAsync_ReturnsPaginationMetaData()
    {
        var expected = new PaginationMetaData
        {
            CurrentPage = 1,
            PageSize = 4,
            TotalCount = 4,
            TotalPages = 1
        };

        _repositoryManagerMock.Location
            .GetAllLocationsAsync(Arg.Any<LocationParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(_locations));

        var result =
            await _serviceManager.LocationService.GetAllLocationsAsync(new LocationParameters(), false);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.metaData.CurrentPage, Is.EqualTo(expected.CurrentPage));
            Assert.That(result.metaData.PageSize, Is.EqualTo(expected.PageSize));
            Assert.That(result.metaData.TotalCount, Is.EqualTo(expected.TotalCount));
            Assert.That(result.metaData.TotalPages, Is.EqualTo(expected.TotalPages));
        });
    }

    [Test]
    public async Task GetLocationByIdAsync_ReturnsLocationDtoWhenLocationExists()
    {
        var id = Guid.NewGuid();

        var testLocation = new Location
        {
            Id = id,
            Name = "Location 1",
            Address = "Address 1"
        };
        
        var expected = new LocationDto
        {
            Id = id,
            Name = testLocation.Name,
            Address = testLocation.Address
        };

        _repositoryManagerMock.Location
            .GetLocationByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testLocation));

        var result = await _serviceManager.LocationService.GetLocationByIdAsync(id, false);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetLocationByIdAsync_ThrowsNotFoundExceptionWhenLocationDoesNotExist()
    {
        _repositoryManagerMock.Location
            .GetLocationByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())
            .ReturnsNull();
        
        Assert.ThrowsAsync<LocationNotFoundException>(async () =>
            await _serviceManager.LocationService.GetLocationByIdAsync(Guid.NewGuid(), false));
    }

    [Test]
    public async Task CreateLocationAsync_ReturnsLocationDto_WhenCreatedNew()
    {
        var location = new LocationForCreationDto { Name = "Location 1", Address = "Address 1" };
        var expected = new LocationDto { Id = Guid.NewGuid(), Name = location.Name, Address = location.Address };   
        
        _repositoryManagerMock.Location.CreateLocation(Arg.Any<Location>());
        
        var result = await _serviceManager.LocationService.CreateLocationAsync(location);
        Assert.Multiple(() =>
        {           
            Assert.That(result.Name, Is.EqualTo(expected.Name));          
            Assert.That(result.Address, Is.EqualTo(expected.Address));           
        });
    }

    [Test]
    public void DeleteLocationAsync_ThrowsLocationNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<LocationNotFoundException>(async () =>
        {
            await _serviceManager.LocationService.DeleteLocationAsync(Guid.NewGuid(), false);
        });

    [Test]
    public async Task DeleteLocationAsync_DeletesExistingLocation()
    {
        var id = Guid.NewGuid();

        var testLocation = new Location { Id = id, Name = "Location 1", Address = "Address 1" };
        
        _repositoryManagerMock.SaveAsync()
            .Returns(Task.CompletedTask);
        _locationRepositoryMock.LocationExists(Arg.Any<Guid>())
            .Returns(true);
        _locationRepositoryMock.GetLocationByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testLocation));
        
        await _serviceManager.LocationService.DeleteLocationAsync(id, false);

        _repositoryManagerMock.ReceivedWithAnyArgs().Location.DeleteLocation(testLocation);
        Console.WriteLine();
    }
    
    [Test]
    public void UpdateLocationAsync_ThrowsLocationNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<LocationNotFoundException>(async () =>
        {
            await _serviceManager.LocationService.UpdateLocationAsync(Guid.NewGuid(), new LocationForUpdateDto(), false);
        });

    [Test]
    public async Task UpdateLocationAsync_DeletesExistingLocation()
    {
        var id = Guid.NewGuid();
    
        var testLocation = new Location { Id = id, Name = "Location 1", Address = "Address 1" };
        
        _repositoryManagerMock.SaveAsync()
            .Returns(Task.CompletedTask);
        _locationRepositoryMock.LocationExists(Arg.Any<Guid>())
            .Returns(true);
        _locationRepositoryMock.GetLocationByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testLocation));
        
        await _serviceManager.LocationService.UpdateLocationAsync(Guid.NewGuid(), new LocationForUpdateDto(), false);
    
        await _repositoryManagerMock.ReceivedWithAnyArgs().SaveAsync();
    }
}