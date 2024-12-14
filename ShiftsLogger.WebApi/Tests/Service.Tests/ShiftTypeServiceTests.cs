using Contracts;
using Contracts.Repository;
using Entities.Exceptions.NotFound;
using Entities.Models;
using NSubstitute;
using Shared.Dto.ShiftType;
using Shared.RequestFeatures;

namespace Service.Tests;

public class ShiftTypeServiceTests
{
    private IShiftTypeRepository _shiftTypeRepositoryMock;
    private IRepositoryManager _repositoryManagerMock;
    private ServiceManager _serviceManager;
    
    [SetUp]
    public void Setup()
    {
        _shiftTypeRepositoryMock = Substitute.For<IShiftTypeRepository>();
        _repositoryManagerMock = Substitute.For<IRepositoryManager>();
        
        _repositoryManagerMock.ShiftType.Returns(_shiftTypeRepositoryMock);
        
        _repositoryManagerMock.SaveAsync()
            .Returns(Task.CompletedTask);

        _serviceManager = new ServiceManager(_repositoryManagerMock);
    }

    [Test]
    public async Task GetAllShiftTypesAsync_ReturnsListOfShiftTypes()
    {
        var testShiftTypes = new PagedList<ShiftType>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new ShiftType
            {
                Id = Guid.NewGuid(),
                Name = "Shift Type 1"
            },
            new ShiftType
            {
                Id = Guid.NewGuid(),
                Name = "Shift Type 2"
            }
        );
        
        var expected = testShiftTypes.Select(st => new ShiftTypeDto
        {
            Id = st.Id,
            Name = st.Name
        }).ToList();
        
        _repositoryManagerMock.ShiftType
            .GetAllShiftTypesAsync(Arg.Any<ShiftTypeParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(testShiftTypes));

        var result =
            await _serviceManager.ShiftTypeService.GetAllShiftTypesAsync(new ShiftTypeParameters(), trackChanges: false);
        
        Assert.That(result.shiftTypes, Is.EquivalentTo(expected));
    }

    [Test]
    public async Task GetAllShiftTypesAsync_ReturnsPaginationMetaData()
    {
        var expected = new PagedList<ShiftType>(
            count: 2, 
            pageNumber: 1, 
            pageSize: 2, 
            new ShiftType
            {
                Id = Guid.NewGuid(),
                Name = "Shift Type 1"
            },
            new ShiftType
            {
                Id = Guid.NewGuid(),
                Name = "Shift Type 2"
            }
        );

        _repositoryManagerMock.ShiftType
            .GetAllShiftTypesAsync(Arg.Any<ShiftTypeParameters>(), Arg.Any<bool>())
            .Returns(Task.FromResult(expected));

        var result =
            await _serviceManager.ShiftTypeService.GetAllShiftTypesAsync(new ShiftTypeParameters(), false);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.metaData.CurrentPage, Is.EqualTo(expected.PaginationMetaData.CurrentPage));
            Assert.That(result.metaData.PageSize, Is.EqualTo(expected.PaginationMetaData.PageSize));
            Assert.That(result.metaData.TotalCount, Is.EqualTo(expected.PaginationMetaData.TotalCount));
            Assert.That(result.metaData.TotalPages, Is.EqualTo(expected.PaginationMetaData.TotalPages));
        });
    }

    [Test]
    public async Task GetShiftTypeByIdAsync_ReturnsShiftTypeDto_WhenShiftTypeExists()
    {
        var id = Guid.NewGuid();

        var testShiftType = new ShiftType
        {
            Id = id,
            Name = "ShiftType 1"
        };
        
        var expected = new ShiftTypeDto
        {
            Id = id,
            Name = testShiftType.Name
        };
        
        _shiftTypeRepositoryMock.ShiftTypeExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftTypeRepositoryMock.GetShiftTypeByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShiftType));

        var result = await _serviceManager.ShiftTypeService.GetShiftTypeByIdAsync(id, false);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetShiftTypeByIdAsync_ThrowsNotFoundException_WhenShiftTypeDoesNotExist() =>
        Assert.ThrowsAsync<ShiftTypeNotFoundException>(async () =>
            await _serviceManager.ShiftTypeService.GetShiftTypeByIdAsync(Guid.NewGuid(), false));

    [Test]
    public async Task CreateShiftTypeAsync_ReturnsShiftTypeDto_WhenCreatedNew()
    {
        var location = new ShiftTypeForCreationDto { Name = "ShiftType 1" };
        var expected = new ShiftTypeDto { Id = Guid.NewGuid(), Name = location.Name };   
        
        _repositoryManagerMock.ShiftType.CreateShiftType(Arg.Any<ShiftType>());
        
        var result = await _serviceManager.ShiftTypeService.CreateShiftTypeAsync(location);
        
        Assert.That(result.Name, Is.EqualTo(expected.Name)); 
    }

    [Test]
    public void DeleteShiftTypeAsync_ThrowsShiftTypeNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<ShiftTypeNotFoundException>(async () =>
            await _serviceManager.ShiftTypeService.DeleteShiftTypeAsync(Guid.NewGuid(), false));

    [Test]
    public async Task DeleteShiftTypeAsync_DeletesExistingShiftType()
    {
        var id = Guid.NewGuid();

        var testShiftType = new ShiftType { Id = id, Name = "ShiftType 1" };
        
        _shiftTypeRepositoryMock.ShiftTypeExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftTypeRepositoryMock.GetShiftTypeByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShiftType));
        
        await _serviceManager.ShiftTypeService.DeleteShiftTypeAsync(id, false);

        _repositoryManagerMock.ReceivedWithAnyArgs().ShiftType.DeleteShiftType(testShiftType);
        Console.WriteLine();
    }
    
    [Test]
    public void UpdateShiftTypeAsync_ThrowsShiftTypeNotFoundException_WhenNoMatches() =>
        Assert.ThrowsAsync<ShiftTypeNotFoundException>(async () =>
        {
            await _serviceManager.ShiftTypeService.UpdateShiftTypeAsync(Guid.NewGuid(), new ShiftTypeForUpdateDto(), false);
        });

    [Test]
    public async Task UpdateShiftTypeAsync_DeletesExistingShiftType()
    {
        var id = Guid.NewGuid();
        
        var testShiftType = new ShiftType { Id = id, Name = "ShiftType 1" };
        
        _shiftTypeRepositoryMock.ShiftTypeExists(Arg.Any<Guid>())
            .Returns(true);
        _shiftTypeRepositoryMock.GetShiftTypeByIdAsync(Arg.Any<Guid>(), Arg.Any<bool>())!
            .Returns(Task.FromResult(testShiftType));
        
        await _serviceManager.ShiftTypeService.UpdateShiftTypeAsync(Guid.NewGuid(), new ShiftTypeForUpdateDto(), false);
        
        await _repositoryManagerMock.ReceivedWithAnyArgs().SaveAsync();
    }
}