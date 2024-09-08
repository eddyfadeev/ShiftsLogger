using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Controller to manage operations related to shift types.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShiftTypesController : ControllerBase
{
    private readonly IShiftTypeService _shiftTypeService;

    public ShiftTypesController(IShiftTypeService shiftTypeService)
    {
        _shiftTypeService = shiftTypeService;
    }

    /// <summary>
    /// Retrieves all shift types.
    /// </summary>
    /// <returns>
    /// A list of shift types if available; otherwise an empty response with status code 204.
    /// </returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ShiftType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllShiftTypes()
    {
        var shiftType = _shiftTypeService.GetAllShiftTypes();

        return shiftType.Count == 0 ? NoContent() : Ok(shiftType);
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
    public IActionResult GetShiftTypeById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var shiftType = _shiftTypeService.GetShiftType(id);
        if (shiftType is not null)
        {
            return Ok(shiftType);
        }
        
        return NotFound($"Shift type with ID: {id} not found");
    }

    /// <summary>
    /// Adds a new shift type.
    /// </summary>
    /// <param name="shiftType">The shift type to add.</param>
    /// <returns>The created shift type with status code 201 if successful, otherwise status code 400.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ShiftType), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddShiftType([FromBody] ShiftType shiftType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _shiftTypeService.AddShiftType(shiftType);
        if (result > 0)
        {
            return CreatedAtAction(
                nameof(GetShiftTypeById), 
                new { id = shiftType.Id }, 
                shiftType
                );
        }
        
        return BadRequest("Failed to add shift type");
    }

    /// <summary>
    /// Updates an existing shift type.
    /// </summary>
    /// <param name="id">The ID of the shift type to update.</param>
    /// <param name="shiftType">The updated shift type information.</param>
    /// <returns>
    /// A status code indicating the result of the operation. 200 OK if successful, 400 Bad Request if the update fails or if the ID does not match.
    /// </returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateShiftType(int id, [FromBody] ShiftType shiftType)
    {
        if (id != shiftType.Id)
        {
            return BadRequest("Shift type ID does not match");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _shiftTypeService.UpdateShiftType(shiftType);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to update shift type");
    }

    /// <summary>
    /// Deletes a shift type by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift type to be deleted.</param>
    /// <returns>
    /// An HTTP 200 OK response if the shift type is successfully deleted;
    /// an HTTP 400 Bad Request response if the deletion fails or the input is invalid.
    /// </returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteShiftType(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var shiftType = _shiftTypeService.GetShiftType(id);
        if (shiftType is null)
        {
            return NotFound($"Shift type with ID: {id} not found");
        }
        
        var result = _shiftTypeService.RemoveShiftType(shiftType);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to remove shift type");
    }
}