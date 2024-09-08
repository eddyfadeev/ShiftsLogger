using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to work shifts, such as fetching, adding, updating, and deleting shifts.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftsLoggerService _shiftsLoggerService;

    public ShiftsController(IShiftsLoggerService shiftsLoggerService)
    {
        _shiftsLoggerService = shiftsLoggerService;
    }

    /// <summary>
    /// Fetches all shifts from the system.
    /// </summary>
    /// <returns>A list of all shifts, or NoContent if no shifts are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllShifts()
    {
        var shifts = _shiftsLoggerService.GetAllShifts();

        return shifts.Count == 0 ? NoContent() : Ok(shifts);
    }

    /// <summary>
    /// Fetches a specific shift by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift to retrieve.</param>
    /// <returns>The shift with the specified ID if found,
    /// or an error response if not found or if the request is invalid.</returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetShiftById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var shift = _shiftsLoggerService.GetShift(id);
        if (shift is not null)
        {
            return Ok(shift);
        }
        
        return NotFound($"Shift with ID: {id} not found");
    }

    /// <summary>
    /// Adds a new shift to the system.
    /// </summary>
    /// <param name="shift">The shift object containing details about the shift to be added.</param>
    /// <returns>An action result indicating the outcome of the operation,
    /// including the created shift on success or an error response on failure.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Shift), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddShift([FromBody] Shift shift)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _shiftsLoggerService.AddShift(shift);
        if (result > 0)
        {
            return CreatedAtAction(
                nameof(GetShiftById), 
                new { id = shift.Id }, 
                shift
                );
        }
        
        return BadRequest("Failed to add shift");
    }

    /// <summary>
    /// Updates an existing shift.
    /// </summary>
    /// <param name="id">The ID of the shift to update.</param>
    /// <param name="shift">The shift object with updated details.</param>
    /// <returns>Returns OK if the update is successful;
    /// BadRequest if the ID does not match or the update fails.</returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateShift(int id, [FromBody] Shift shift)
    {
        if (id != shift.Id)
        {
            return BadRequest("Shift ID does not match");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _shiftsLoggerService.UpdateShift(shift);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to update shift");
    }

    /// <summary>
    /// Deletes a shift by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift to delete.</param>
    /// <returns>A status indicating the result of the operation.</returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteShift(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var shift = _shiftsLoggerService.GetShift(id);
        if (shift is null)
        {
            return NotFound($"Shift with ID: {id} not found");
        }
        
        var result = _shiftsLoggerService.RemoveShift(shift);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to remove shift");
    }
}