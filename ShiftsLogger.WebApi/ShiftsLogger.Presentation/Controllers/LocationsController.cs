using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.RequestFeatures;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/locations")]
[ApiController]
[OutputCache(PolicyName = "15MinsExpiry")]
public class LocationsController : ControllerBase
{
    private readonly IServiceManager _service;

    public LocationsController(IServiceManager service) =>
        _service = service;
    
    [HttpGet("{locationId:guid}/shifts")]
    public async Task<IActionResult> GetShiftsForLocation(Guid locationId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForLocation(locationId, requestParameters, trackChanges: false);
        
        var etag = $"\"{Guid.NewGuid():n}\"";
        
        Response.Headers.ETag = etag;
        Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}