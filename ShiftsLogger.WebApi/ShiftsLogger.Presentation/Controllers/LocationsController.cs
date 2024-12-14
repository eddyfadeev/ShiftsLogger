using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.Location;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.ActionFilters;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/locations")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly IServiceManager _service;

    public LocationsController(IServiceManager service) =>
        _service = service;

    [HttpGet]
    public async Task<IActionResult> GetLocations([FromQuery] LocationParameters locationParameters)
    {
        var pagedResult = await _service.LocationService
            .GetAllLocationsAsync(locationParameters, trackChanges: false);

        this.SetPaginationMetadata(pagedResult.metaData);

        return Ok(pagedResult.locations);
    }

    [HttpGet("{locationId:guid}", Name = "GetLocationById")]
    public async Task<IActionResult> GetLocationById(Guid locationId)
    {
        var location = await _service.LocationService.GetLocationByIdAsync(locationId, trackChanges: false);

        return Ok(location);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateLocation([FromBody] LocationForCreationDto location)
    {
        var createdLocation = await _service.LocationService.CreateLocationAsync(location);

        return CreatedAtRoute
        (
            "GetLocationById",
            new { locationId = createdLocation.Id },
            createdLocation
        );
    }

    [HttpDelete("{locationId:guid}")]
    public async Task<IActionResult> DeleteLocation(Guid locationId)
    {
        await _service.LocationService.DeleteLocationAsync(locationId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{locationId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateLocation(Guid locationId, [FromBody] LocationForUpdateDto location)
    {
        await _service.LocationService.UpdateLocationAsync(locationId, location, trackChanges: true);
        return NoContent();
    }
}