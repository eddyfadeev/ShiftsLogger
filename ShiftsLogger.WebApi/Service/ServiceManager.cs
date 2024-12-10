using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ILocationService> _locationService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IShiftService> _shiftService;
    private readonly Lazy<IShiftTypeService> _shiftTypeService;

    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _locationService = new Lazy<ILocationService>(() =>
            new LocationService(repositoryManager, logger));

        _userService = new Lazy<IUserService>(() =>
            new UserService(repositoryManager, logger));

        _shiftService = new Lazy<IShiftService>(() =>
            new ShiftService(repositoryManager, logger));

        _shiftTypeService = new Lazy<IShiftTypeService>(() =>
            new ShiftTypeService(repositoryManager, logger));
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