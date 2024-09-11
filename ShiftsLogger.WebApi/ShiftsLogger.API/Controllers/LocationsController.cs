using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Extensions;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Extensions;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Dto;
using ShiftsLogger.Domain.Models.Entity;
using ShiftsLogger.Infrastructure.Extensions;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// LocationsController handles all HTTP requests related to working location management.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class LocationsController : BaseController<Location>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public LocationsController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private protected override int GetEntityId(Location entity) => entity.Id;

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet("all")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Location>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override async Task<IActionResult> GetAllEntities()
    {
        var locations = await _unitOfWork.Repository<Location>().GetAsync(); 
                

        return locations.Count > 0 ? Ok(locations) : NoContent();
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
    public override async Task<IActionResult> GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var location = await _unitOfWork.Repository<Location>().GetByIdAsync(id);
        if (location is null)
        {
            return NotFound($"Location with ID: {id} not found");
        }
        
        return Ok(location);
    }

    /// <summary>
    /// Retrieves all shifts associated with a specific location by their unique identifier.
    /// </summary>
    /// <param name="locationId">The unique identifier of the location whose shifts are to be retrieved.</param>
    /// <returns>An IActionResult containing a list of shifts if found;
    /// otherwise, returns a NotFound or BadRequest result.</returns>
    [HttpGet("shifts/{locationId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetShiftsByLocation(int locationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var location = await _unitOfWork.Repository<Location>().GetByIdAsync(
            locationId,
            includeProperties: "Shifts"
                );

        if (location is null)
        {
            return NotFound($"Location with ID: {locationId} not found");
        }
        
        if (location.Shifts?.Count == 0 || location.Shifts is null)
        {
            return NotFound("No shifts found for this location");
        }

        var locationDto = location.MapLocationToDto() with
        {
            Shifts = location.Shifts.Select(s => s.MapShiftToDto()).ToList()
        };
        
        return Ok(locationDto);
    }
}