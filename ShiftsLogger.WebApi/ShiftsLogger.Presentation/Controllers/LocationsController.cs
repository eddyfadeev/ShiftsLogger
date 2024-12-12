using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/locations")]
[ApiController]
[OutputCache(PolicyName = "15MinsExpiry")]
public class LocationsController : ControllerBase
{
    private readonly IServiceManager _service;

    public LocationsController(IServiceManager service) =>
        _service = service;

    [HttpGet]
    public async Task<IActionResult> GetLocations([FromQuery] LocationParameters locationParameters)
    {
        var pagedResult = await _service.LocationService.GetAllLocationsAsync(locationParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);

        return Ok(pagedResult.locations);
    }
    
    [HttpGet("{locationId:guid}/shifts")]
    public async Task<IActionResult> GetShiftsForLocation(Guid locationId,
        [FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult =
            await _service.ShiftService.GetShiftsForLocation(locationId, requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }
}