using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ILocationService> _locationService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IShiftService> _shiftService;
    private readonly Lazy<IShiftTypeService> _shiftTypeService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _locationService = new Lazy<ILocationService>(() =>
            new LocationService(repositoryManager));

        _userService = new Lazy<IUserService>(() =>
            new UserService(repositoryManager));

        _shiftService = new Lazy<IShiftService>(() =>
            new ShiftService(repositoryManager));

        _shiftTypeService = new Lazy<IShiftTypeService>(() =>
            new ShiftTypeService(repositoryManager));
    }
    
    public IShiftService ShiftService => 
        _shiftService.Value;

    public IUserService UserService =>
        _userService.Value;

    public IShiftTypeService ShiftTypeService =>
        _shiftTypeService.Value;

    public ILocationService LocationService =>
        _locationService.Value;
}