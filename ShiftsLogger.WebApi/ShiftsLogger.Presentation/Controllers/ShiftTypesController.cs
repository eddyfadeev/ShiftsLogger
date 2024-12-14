using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.ShiftType;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.ActionFilters;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/shift-types")]
public class ShiftTypesController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public ShiftTypesController(IServiceManager service) =>
        _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetAllShiftTypesAsync([FromQuery] ShiftTypeParameters shiftTypeParameters)
    {
        var pagedResult = await _service.ShiftTypeService
            .GetAllShiftTypesAsync(shiftTypeParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);

        return Ok(pagedResult.shiftTypes);
    }

    [HttpGet("{shiftTypeId:guid}", Name = "GetShiftTypeById")]
    public async Task<IActionResult> GetShiftTypeById(Guid shiftTypeId)
    {
        var shiftType = await _service.ShiftTypeService.GetShiftTypeByIdAsync(shiftTypeId, trackChanges: false);

        return Ok(shiftType);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateShiftType([FromBody] ShiftTypeForCreationDto shiftType)
    { 
        var createdShiftType = await _service.ShiftTypeService.CreateShiftTypeAsync(shiftType);

        return CreatedAtRoute
        (
            "GetShiftTypeById",
            new { shiftTypeId = createdShiftType.Id },
            createdShiftType
        );
    }

    [HttpDelete("{shiftTypeId:guid}")]
    public async Task<IActionResult> DeleteShiftType(Guid shiftTypeId)
    {
        await _service.ShiftTypeService.DeleteShiftTypeAsync(shiftTypeId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{shiftTypeId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateShiftType(Guid shiftTypeId, [FromBody] ShiftTypeForUpdateDto shiftType)
    {
        await _service.ShiftTypeService.UpdateShiftTypeAsync(shiftTypeId, shiftType, trackChanges: true);
        return NoContent();
    }
}