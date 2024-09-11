using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Controller to manage operations related to shift types.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ShiftTypesController : BaseController<ShiftType>
{
    private readonly IUnitOfWork<ShiftType> _unitOfWork;

    public ShiftTypesController(IUnitOfWork<ShiftType> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private protected override int GetEntityId(ShiftType entity) => entity.Id;
    
    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ShiftType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override async Task<IActionResult> GetAllEntities()
    {
        var shiftsTypes = await _unitOfWork.Repository.GetAsync(); 

        return shiftsTypes.Count > 0 ? Ok(shiftsTypes) : NoContent();
    }
    
    /// <summary>
    /// Retrieves a shift type by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift type to retrieve.</param>
    /// <returns>
    /// The shift type corresponding to the specified ID if found;
    /// otherwise a response with status code 404 if not found or
    /// status code 400 for a bad request.
    /// </returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ShiftType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var shiftType = await _unitOfWork.Repository.GetByIdAsync(id);
        if (shiftType is not null)
        {
            return Ok(shiftType);
        }
        
        return NotFound($"Shift type with ID: {id} not found");
    }
}