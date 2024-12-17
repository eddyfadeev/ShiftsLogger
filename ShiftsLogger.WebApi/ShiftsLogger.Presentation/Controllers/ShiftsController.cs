using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Dto.Shift;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.ActionFilters;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/v{version:apiVersion}/shifts")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly IServiceManager _service;

    public ShiftsController(IServiceManager service) =>
        _service = service;

    [HttpGet]
    public async Task<IActionResult> GetShifts([FromQuery] ShiftParameters requestParameters)
    {
        var pagedResult = await _service.ShiftService.GetAllShiftsAsync(requestParameters, trackChanges: false);
        
        this.SetPaginationMetadata(pagedResult.metaData);
        
        return Ok(pagedResult.shifts);
    }

    [HttpGet("{shiftId:guid}", Name = "GetShiftById")]
    public async Task<IActionResult> GetShiftById(Guid shiftId)
    {
        var shift = await _service.ShiftService.GetShiftByIdAsync(shiftId, trackChanges: false);
        
        return Ok(shift);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateShift([FromBody] ShiftForCreationDto shift)
    {
        var createdShift = await _service.ShiftService.CreateShiftAsync(shift);

        return CreatedAtRoute
        (
            "GetShiftById",
            new { shiftId = createdShift.Id },
            createdShift
        );
    }

    [HttpDelete("{shiftId:guid}")]
    public async Task<IActionResult> DeleteShift(Guid shiftId)
    {
        await _service.ShiftService.DeleteShiftAsync(shiftId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{shiftId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateShift(Guid shiftId, [FromBody] ShiftForUpdateDto shift)
    {
        await _service.ShiftService.UpdateShiftAsync(shiftId, shift, trackChanges: true);
        return NoContent();
    }
}