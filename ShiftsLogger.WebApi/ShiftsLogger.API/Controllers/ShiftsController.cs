using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to work shifts, such as fetching, adding, updating, and deleting shifts.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ShiftsController : BaseController<Shift>
{
    private readonly IUnitOfWork<Shift> _unitOfWork;

    public ShiftsController(IUnitOfWork<Shift> unitOfWork) : base(unitOfWork)
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
        var shifts = await _unitOfWork.Repository.GetAsync(); 
        
        return shifts.Count > 0 ? Ok(shifts) : NoContent();
        }
    
    [HttpGet("user/{userId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetShiftsByUser(int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isUserFound = await _unitOfWork.Repository.ExistsAsync(
            userId, s =>
                s.UserId == userId
            );
        
        if (!isUserFound)
        {
            return NotFound($"User with ID: {userId} not found");
        }
        
        var shiftsByUser = await _unitOfWork.Repository.GetAsync(
            s => 
                s.UserId == userId
        );
        
        return shiftsByUser.Count > 0? Ok(shiftsByUser) : NoContent();
    }
    
    [HttpGet("location/{locationId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetShiftsByLocation(int locationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isLocationFound = await _unitOfWork.Repository.ExistsAsync(
            locationId, s => 
                s.LocationId == locationId
        );
        
        if (!isLocationFound)
        {
            return NotFound($"Location with ID: {locationId} not found");
        }

        var shiftsByLocation = await _unitOfWork.Repository.GetAsync(
            s => 
                s.LocationId == locationId
        );
        
        return shiftsByLocation.Count > 0? Ok(shiftsByLocation) : NoContent();
    }

    [HttpGet("shiftType/{shiftTypeId:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetShiftsByShiftType(int shiftTypeId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isShiftTypeFound = await _unitOfWork.Repository.ExistsAsync(
            shiftTypeId, s => 
                s.ShiftTypeId == shiftTypeId
            );
        
        if (!isShiftTypeFound)
        {
            return NotFound($"Shift type with ID: {shiftTypeId} not found");
        }
        
        var shiftsByShiftType = await _unitOfWork.Repository.GetAsync(
            s => 
                s.ShiftTypeId == shiftTypeId
        );
        
        return shiftsByShiftType.Count > 0? Ok(shiftsByShiftType) : NoContent();
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
        
        var entity = await _unitOfWork.Repository.GetByIdAsync(id);
        if (entity is not null)
        {
            return Ok(entity);
        }
        
        return NotFound($"Shift with ID: {id} not found");
    }
    
    private protected override int GetEntityId(Shift entity) => entity.Id;
}