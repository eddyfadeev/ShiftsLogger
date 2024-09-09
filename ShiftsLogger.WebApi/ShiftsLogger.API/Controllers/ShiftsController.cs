using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to work shifts, such as fetching, adding, updating, and deleting shifts.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShiftsController : BaseController<Shift>
{
    private readonly IUnitOfWork<Shift> _unitOfWork;

    public ShiftsController(IUnitOfWork<Shift> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private protected override int GetEntityId(Shift entity) => entity.Id;

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override IActionResult GetAllEntities()
    {
        var shifts = _unitOfWork.Repository.Get(); 
        
        return shifts.Count > 0 ? Ok(shifts) : NoContent();
        }
    
    /// <summary>
    /// Fetches a specific entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity with the specified ID if found,
    /// or an error response if not found or if the request is invalid.</returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override IActionResult GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var entity = _unitOfWork.Repository.GetById(id);
        if (entity is not null)
        {
            return Ok(entity);
        }
        
        return NotFound($"Shift with ID: {id} not found");
    }
}