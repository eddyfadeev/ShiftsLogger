using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// LocationsController handles all HTTP requests related to working location management.
/// </summary>
[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// Retrieves all work locations.
    /// </summary>
    /// <returns>A list of all locations or a NoContent status if no locations are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Location>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllLocations()
    {
        var locations = _locationService.GetAllLocations();

        return locations.Count == 0 ? NoContent() : Ok(locations);
    }

    /// <summary>
    /// Retrieves a work location specified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the location.</param>
    /// <returns>The location if found, a NotFound status if the location does not exist, or a BadRequest status if the request is invalid.</returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetLocationById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var location = _locationService.GetLocation(id);
        if (location is not null)
        {
            return Ok(location);
        }
        
        return NotFound($"Location with ID: {id} not found");
    }

    /// <summary>
    /// Adds a new location.
    /// </summary>
    /// <param name="location">A work location to be added.</param>
    /// <returns>A status indicating the result of the operation.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
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

    /// <summary>
    /// Updates the specified work location with the provided new details.
    /// </summary>
    /// <param name="id">The ID of the location to be updated.</param>
    /// <param name="location">The updated location details.</param>
    /// <returns>Returns a status indicating whether the update was successful or not.</returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
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

    /// <summary>
    /// Deletes a work location by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the location to be deleted.</param>
    /// <returns>Returns an OK status if the location is successfully deleted, a NotFound status if the location is not found, or a BadRequest status if the request is invalid.</returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteLocation(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
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