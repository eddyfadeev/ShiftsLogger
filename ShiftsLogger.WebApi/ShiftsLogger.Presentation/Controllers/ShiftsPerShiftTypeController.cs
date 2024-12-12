using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/shift-types/{shiftTypeId:guid}/shifts")]
[ApiController]
[OutputCache(PolicyName = "15MinsExpiry")]
public class ShiftsPerShiftTypeController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ShiftsPerShiftTypeController(IServiceManager service) =>
        _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetShiftsForShiftType(Guid shiftTypeId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForShiftType(shiftTypeId, requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}