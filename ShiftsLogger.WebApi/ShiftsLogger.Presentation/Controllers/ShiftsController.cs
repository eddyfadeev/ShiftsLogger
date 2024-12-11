using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/shifts")]
[Route("api/shift-types/{shiftTypeId:guid}/shifts")]
[Route("api/users/{userId:guid}/shifts")]
[ApiController]
[OutputCache(PolicyName = "15MinsExpiry")]
public class ShiftsController : ControllerBase
{
    private readonly IServiceManager _service;

    public ShiftsController(IServiceManager service) =>
        _service = service;

    [HttpGet]
    public async Task<IActionResult> GetShifts([FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult = await _service.ShiftService.GetAllShiftsAsync(requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }

    [HttpGet]
    [Route("api/{locationId:guid}/shifts")]
    public async Task<IActionResult> GetShiftsForLocation(Guid locationId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForLocation(locationId, requestParameters, trackChanges: false);

        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}