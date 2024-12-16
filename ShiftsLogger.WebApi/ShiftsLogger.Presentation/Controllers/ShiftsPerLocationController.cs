using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/locations/{locationId:guid}/shifts")]
[ApiController]
public class ShiftsPerLocationController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ShiftsPerLocationController(IServiceManager service) =>
        _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetShiftsForLocation(Guid locationId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForLocationAsync(locationId, requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}