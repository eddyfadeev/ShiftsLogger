using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/users/{userId:guid}/shifts")]
[ApiController]
public class ShiftsPerUserController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ShiftsPerUserController(IServiceManager service) =>
        _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetShiftsForUser(Guid userId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForUser(userId, requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}