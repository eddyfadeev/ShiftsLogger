using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/shifts")]
[Route("api/locations/{locationId:guid}/shifts")]
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

        if (pagedResult.shifts.Count <= 0)
        {
            return NoContent();
        }
        
        var etag = $"\"{Guid.NewGuid():n}\"";
        
        Response.Headers.ETag = etag;
        Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }

}