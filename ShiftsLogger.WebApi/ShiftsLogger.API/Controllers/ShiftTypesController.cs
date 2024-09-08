using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftTypesController : ControllerBase
{
    private readonly IShiftTypeService _shiftTypeService;

    public ShiftTypesController(IShiftTypeService shiftTypeService)
    {
        _shiftTypeService = shiftTypeService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<ShiftType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllShiftTypes()
    {
        var shiftType = _shiftTypeService.GetAllShiftTypes();

        return shiftType.Count == 0 ? NoContent() : Ok(shiftType);
    }

    [HttpGet("{id:int}")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
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