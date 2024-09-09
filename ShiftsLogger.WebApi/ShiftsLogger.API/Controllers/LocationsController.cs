using System.Data;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// LocationsController handles all HTTP requests related to working location management.
/// </summary>
[ApiController]
[Route("[controller]")]
public class LocationsController : BaseController<Location>
{
    private readonly IUnitOfWork<Location> _unitOfWork;
    
    public LocationsController(IUnitOfWork<Location> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private protected override int GetEntityId(Location entity) => entity.Id;

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Location>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override IActionResult GetAllEntities()
    {
        var locations = from loc in _unitOfWork.Repository.Get() 
            select loc; 
                

        return locations.Any() ? Ok(locations) : NoContent();
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
    public override IActionResult GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var location = _unitOfWork.Repository.GetById(id);
        if (location is not null)
        {
            return Ok(location);
        }
        
        return NotFound($"Location with ID: {id} not found");
    }
}