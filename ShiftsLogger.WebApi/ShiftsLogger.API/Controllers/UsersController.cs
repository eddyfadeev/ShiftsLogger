using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Extensions;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entity;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to Users.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : BaseController<User>
{
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override async Task<IActionResult> GetAllEntities()
    {
        var users = await _unitOfWork.Repository<User>().GetAsync();

        return users.Count > 0 ? Ok(users) : NoContent();
    }
    
    /// <summary>
    /// Retrieves all shifts associated with a specific user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose shifts are to be retrieved.</param>
    /// <returns>An IActionResult containing a list of shifts if found;
    /// otherwise, returns a NotFound or BadRequest result.</returns>
    [HttpGet("{userId:int}/shifts")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetShiftsByUserId(int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _unitOfWork.Repository<User>().GetByIdAsync(
            userId,
            includeProperties: "Shifts"
        );
    
        if (user is null)
        {
            return NotFound($"User with ID: {userId} not found");
        }
    
        if (user.Shifts?.Count == 0 || user.Shifts is null)
        {
            return NotFound("No shifts found for this user");
        }

        var userDto = user.MapUserToDto() with
        {
            Shifts = user.Shifts.Select(s => s.MapShiftToDto()).ToList()
        };
    
        return Ok(userDto);
    }
    
    /// <summary>
    /// Retrieves a user from the system by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>An IActionResult containing the User object if found; otherwise, returns a NotFound or BadRequest result.</returns>
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
        if (user is not null)
        {
            return Ok(user);
        }
        
        return NotFound($"User with ID: {id} not found");
    }
    
    private protected override int GetEntityId(User entity) => entity.Id;
}