using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftsLoggerService _shiftsLoggerService;

    public ShiftsController(IShiftsLoggerService shiftsLoggerService)
    {
        _shiftsLoggerService = shiftsLoggerService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllShifts()
    {
        var shifts = _shiftsLoggerService.GetAllShifts();

        return shifts.Count == 0 ? NoContent() : Ok(shifts);
    }

    [HttpGet("{id:int}")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
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