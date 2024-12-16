using Contracts;
using Contracts.Repository;
using Entities.Exceptions.BadRequest;
using Entities.Exceptions.NotFound;
using Entities.Models;
using NSubstitute;
using Shared.Dto.Shift;
using Shared.RequestFeatures;

namespace Service.Tests;

public class ShiftServiceTests
{
    private IShiftRepository _shiftRepositoryMock;
    private IRepositoryManager _repositoryManagerMock;
    private ServiceManager _serviceManager;

    [SetUp]
    public void Setup()
    {
        _shiftRepositoryMock = Substitute.For<IShiftRepository>();
        _repositoryManagerMock = Substitute.For<IRepositoryManager>();
        _repositoryManagerMock.Shift.Returns(_shiftRepositoryMock);
        _repositoryManagerMock.SaveAsync().Returns(Task.CompletedTask);

        _serviceManager = new ServiceManager(_repositoryManagerMock);
    }
    
    [Test]
    public async Task GetAllShiftsAsync_ReturnsListOfShifts()
    {
        var testShifts = new PagedList<Shift>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1",
                LocationId = Guid.NewGuid(),
                ShiftTypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            },
            new Shift
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1).AddHours(-8),
                EndTime = DateTime.Now.AddDays(-1),
                Description = "Test 2",
                LocationId = Guid.NewGuid(),
                ShiftTypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            }
        );
        
        var expected = testShifts.Select(u => new ShiftDto
        {
            Id = u.Id,
            StartTime = u.StartTime,
            EndTime = u.EndTime,
            Description = u.Description,
            HoursWorked = 0,
            Location = string.Empty,
            User = string.Empty,
            ShiftType = string.Empty
        }).ToList();
        
        _repositoryManagerMock.Shift
            .GetAllShiftsAsync(Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetAllShiftsAsync(new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.shifts[0].Id, Is.EqualTo(expected[0].Id));
    }
    
    [Test]
    public void GetAllShiftsAsync_ThrowsIncorrectMaxWorkedHoursBadRequestException()
    {
        var shiftParams = new ShiftParameters
        {
            MinWorkedHours = 5,
            MaxWorkedHours = 3,
        };
        
        Assert.ThrowsAsync<IncorrectMaxWorkedHoursBadRequestException>(async () =>
            await _serviceManager.ShiftService.GetAllShiftsAsync(shiftParams, trackChanges: false));
    }

    [Test]
    public async Task GetAllShiftsAsync_ReturnsPaginationMetaData()
    {
        var expected = new PagedList<Shift>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1",
                LocationId = Guid.NewGuid(),
                ShiftTypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            },
            new Shift
            {
                Id = Guid.NewGuid(),
                StartTime = DateTime.Now.AddDays(-1).AddHours(-8),
                EndTime = DateTime.Now.AddDays(-1),
                Description = "Test 2",
                LocationId = Guid.NewGuid(),
                ShiftTypeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            }
        );
    
        _repositoryManagerMock.Shift
            .GetAllShiftsAsync(Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(expected));
    
        var result =
            await _serviceManager.ShiftService.GetAllShiftsAsync(new ShiftParameters(), false);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.metaData.CurrentPage, Is.EqualTo(expected.PaginationMetaData.CurrentPage));
            Assert.That(result.metaData.PageSize, Is.EqualTo(expected.PaginationMetaData.PageSize));
            Assert.That(result.metaData.TotalCount, Is.EqualTo(expected.PaginationMetaData.TotalCount));
            Assert.That(result.metaData.TotalPages, Is.EqualTo(expected.PaginationMetaData.TotalPages));
        });
    }
    
    [Test]
    public async Task GetShiftsForShiftTypeAsync_ReturnsListOfShifts()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.ShiftType
            .ShiftTypeExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForShiftType(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForShiftTypeAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.shifts[0].StartTime, Is.EqualTo(testShifts[0].StartTime));
    }
    
    [Test]
    public async Task GetShiftsForShiftTypeAsync_ReturnsCorrectMetadata()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.ShiftType
            .ShiftTypeExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForShiftType(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForShiftTypeAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.metaData.PageSize, Is.EqualTo(testShifts.PaginationMetaData.PageSize));
    }
    
    [Test]
    public void GetShiftsForShiftTypeAsync_ThrowsShiftTypeNotFoundException()
    {
        Assert.ThrowsAsync<ShiftTypeNotFoundException>(async () =>
            await _serviceManager.ShiftService.GetShiftsForShiftTypeAsync(Guid.NewGuid(), new ShiftParameters(), false));
    }
    
    [Test]
    public async Task GetShiftsForUserAsync_ReturnsListOfShifts()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.User
            .UserExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForUser(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForUserAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.shifts[0].StartTime, Is.EqualTo(testShifts[0].StartTime));
    }
    
    [Test]
    public async Task GetShiftsForUserAsync_ReturnsCorrectMetadata()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.User
            .UserExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForUser(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForUserAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.metaData.PageSize, Is.EqualTo(testShifts.PaginationMetaData.PageSize));
    }
    
    [Test]
    public void GetShiftsForUserAsync_ThrowsUserNotFoundException()
    {
        Assert.ThrowsAsync<UserNotFoundException>(async () =>
            await _serviceManager.ShiftService.GetShiftsForUserAsync(Guid.NewGuid(), new ShiftParameters(), false));
    }
    
    [Test]
    public async Task GetShiftsForLocationAsync_ReturnsListOfShifts()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.Location
            .LocationExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForLocation(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForLocationAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.shifts[0].StartTime, Is.EqualTo(testShifts[0].StartTime));
    }
    
    [Test]
    public async Task GetShiftsForLocationAsync_ReturnsCorrectMetadata()
    {
        var testShifts = new PagedList<Shift>(
            count: 1, 
            pageNumber: 1, 
            pageSize: 2, 
            new Shift
            {
                StartTime = DateTime.Now.AddHours(-8),
                EndTime = DateTime.Now,
                Description = "Test 1"
            }
        );

        _repositoryManagerMock.Location
            .LocationExists(Arg.Any<Guid>())
            .Returns(true);
        
        _repositoryManagerMock.Shift
            .GetShiftsForLocation(Arg.Any<Guid>(),Arg.Any<ShiftParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShifts));

        var result =
            await _serviceManager.ShiftService.GetShiftsForLocationAsync(Guid.Empty, new ShiftParameters(), trackChanges: false);
        
        Assert.That(result.metaData.PageSize, Is.EqualTo(testShifts.PaginationMetaData.PageSize));
    }
    
    [Test]
    public void GetShiftsForLocationAsync_ThrowsLocationNotFoundException()
    {
        Assert.ThrowsAsync<LocationNotFoundException>(async () =>
            await _serviceManager.ShiftService.GetShiftsForLocationAsync(Guid.NewGuid(), new ShiftParameters(), false));
    }
    
    [Test]
    public async Task GetShiftByIdAsync_ReturnsShiftDto_WhenShiftExists()
    {
        var id = Guid.NewGuid();
    
        var testShift = new Shift
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-8),
            EndTime = DateTime.Now,
            Description = "Test 1",
            LocationId = Guid.NewGuid(),
            ShiftTypeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        
        var expected = new ShiftDto
        {
            Id = testShift.Id,
            StartTime = testShift.StartTime,
            EndTime = testShift.EndTime,
            Description = testShift.Description,
            HoursWorked = 0,
            Location = string.Empty,
            User = string.Empty,
            ShiftType = string.Empty
        };
        
        _shiftRepositoryMock.ShiftExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftRepositoryMock.GetShiftByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShift));
    
        var result = await _serviceManager.ShiftService.GetShiftByIdAsync(id, false);
        
        Assert.That(result.Id, Is.EqualTo(expected.Id));
    }
    
    [Test]
    public void GetShiftByIdAsync_ThrowsNotFoundException_WhenShiftDoesNotExist() =>
        Assert.ThrowsAsync<ShiftNotFoundException>(async () =>
            await _serviceManager.ShiftService.GetShiftByIdAsync(Guid.NewGuid(), false));
    
    [Test]
    public async Task CreateShiftAsync_ReturnsShiftDto_WhenCreatedNew()
    {
        var shift = new ShiftForCreationDto
        {
            StartTime = DateTime.Now.AddHours(-8),
            EndTime = DateTime.Now,
            Description = "Test 1",
            HoursWorked = 8.0m, 
            LocationId = Guid.NewGuid(),
            ShiftTypeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        
        var expected = new ShiftDto
        {
            Id = Guid.Empty,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            Description = shift.Description,
            HoursWorked = shift.HoursWorked,
            Location = string.Empty,
            User = string.Empty,
            ShiftType = string.Empty
        };   
        
        _repositoryManagerMock.Shift.CreateShift(Arg.Any<Shift>());
        _repositoryManagerMock.User.UserExists(Arg.Any<Guid>()).Returns(true);
        _repositoryManagerMock.ShiftType.ShiftTypeExists(Arg.Any<Guid>()).Returns(true);
        _repositoryManagerMock.Location.LocationExists(Arg.Any<Guid>()).Returns(true);
        
        var result = await _serviceManager.ShiftService.CreateShiftAsync(shift);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.StartTime, Is.EqualTo(expected.StartTime));
            Assert.That(result.EndTime, Is.EqualTo(expected.EndTime));
            Assert.That(result.Description, Is.EqualTo(expected.Description));
        }); 
    }
    
    [Test]
    public void DeleteShiftAsync_ThrowsShiftNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<ShiftNotFoundException>(async () =>
            await _serviceManager.ShiftService.DeleteShiftAsync(Guid.NewGuid(), false));
    
    [Test]
    public async Task DeleteShiftAsync_DeletesExistingShift()
    {
        var id = Guid.NewGuid();
    
        var testShift = new Shift 
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-8),
            EndTime = DateTime.Now,
            Description = "Test 1",
            LocationId = Guid.NewGuid(),
            ShiftTypeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        
        _shiftRepositoryMock.ShiftExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftRepositoryMock.GetShiftByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShift));
        
        await _serviceManager.ShiftService.DeleteShiftAsync(id, false);
    
        _repositoryManagerMock.ReceivedWithAnyArgs().Shift.DeleteShift(testShift);
        Console.WriteLine();
    }
    
    [Test]
    public void UpdateShiftAsync_ThrowsShiftNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<ShiftNotFoundException>(async () =>
        {
            await _serviceManager.ShiftService.UpdateShiftAsync(Guid.NewGuid(), new ShiftForUpdateDto(), false);
        });
    
    [Test]
    public async Task UpdateShiftAsync_DeletesExistingShift()
    {
        var testShift = new Shift 
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.Now.AddHours(-8),
            EndTime = DateTime.Now,
            Description = "Test 1",
            LocationId = Guid.NewGuid(),
            ShiftTypeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        
        _shiftRepositoryMock.ShiftExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftRepositoryMock.GetShiftByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShift));
        
        await _serviceManager.ShiftService.UpdateShiftAsync(Guid.NewGuid(), new ShiftForUpdateDto(), false);
        
        await _repositoryManagerMock.ReceivedWithAnyArgs().SaveAsync();
    }
}