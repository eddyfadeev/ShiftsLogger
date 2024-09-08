using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Location>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllLocations()
    {
        var locations = _locationService.GetAllLocations();

        return locations.Count == 0 ? NoContent() : Ok(locations);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetLocationById(int id)
    {
        var location = _locationService.GetLocation(id);
        if (location is not null)
        {
            return Ok(location);
        }
        
        return NotFound($"Location with ID: {id} not found");
    }

    [HttpPost]
    [ProducesResponseType(typeof(Location), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddLocation([FromBody] Location location)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _locationService.AddLocation(location);
        if (result > 0)
        {
            return CreatedAtAction(
                nameof(GetLocationById), 
                new { id = location.Id }, 
                location
                );
        }
        
        return BadRequest("Failed to add location");
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateLocation(int id, [FromBody] Location location)
    {
        if (id != location.Id)
        {
            return BadRequest("Location ID does not match");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _locationService.UpdateLocation(location);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to update location");
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteLocation(int id)
    {
        var location = _locationService.GetLocation(id);
        if (location is null)
        {
            return NotFound($"Location with ID: {id} not found");
        }
        
        var result = _locationService.RemoveLocation(location);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to remove location");
    }
}