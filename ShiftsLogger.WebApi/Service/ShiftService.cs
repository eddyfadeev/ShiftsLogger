using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class ShiftService : IShiftService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public ShiftService(IRepositoryManager repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
}