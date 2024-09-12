using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Extensions;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Dto;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to work shifts, such as fetching, adding, updating, and deleting shifts.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ShiftsController : BaseController<Shift>
{
    private readonly IUnitOfWork _unitOfWork;

    public ShiftsController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override async Task<IActionResult> GetAllEntities()
    {
        var shifts = await _unitOfWork.Repository<Shift>().GetAsync();
        var shiftsDto = shifts.Select(s => s.MapShiftToDto()).ToList();
        
        return shifts.Count > 0 ? Ok(shiftsDto) : NoContent();
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
    public override async Task<IActionResult> GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var entity = await _unitOfWork.Repository<Shift>().GetByIdAsync(id);
        if (entity is not null)
        {
            return Ok(entity);
        }
        
        return NotFound($"Shift with ID: {id} not found");
    }
    
    private protected override int GetEntityId(Shift entity) => entity.Id;
}