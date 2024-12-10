using Contracts;
using Contracts.Repository;

namespace Repository;

public sealed class RepositoryManger : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    
    private readonly Lazy<ILocationRepository> _locationRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IShiftTypeRepository> _shiftTypeRepository;
    private readonly Lazy<IShiftRepository> _shiftRepository;

    public RepositoryManger(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _locationRepository = new Lazy<ILocationRepository>(() => 
            new LocationRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(() =>
            new UserRepository(repositoryContext));
        _shiftTypeRepository = new Lazy<IShiftTypeRepository>(() =>
            new ShiftTypeRepository(repositoryContext));
        _shiftRepository = new Lazy<IShiftRepository>(() =>
            new ShiftRepository(repositoryContext));
    }


    public ILocationRepository Location => 
        _locationRepository.Value;

    public IUserRepository User =>
        _userRepository.Value;

    public IShiftTypeRepository ShiftType =>
        _shiftTypeRepository.Value;

    public IShiftRepository Shift =>
        _shiftRepository.Value;

    public async Task SaveAsync() =>
        await _repositoryContext.SaveChangesAsync();
}