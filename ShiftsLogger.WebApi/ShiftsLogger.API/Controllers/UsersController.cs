using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to Users.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : BaseController<User>
{
    private readonly IUnitOfWork<User> _unitOfWork;

    public UsersController(IUnitOfWork<User> unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private protected override int GetEntityId(User entity) => entity.Id;

    /// <summary>
    /// Fetches all entities from the system.
    /// </summary>
    /// <returns>A list of all entities, or NoContent if no entities are found.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ShiftType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public override IActionResult GetAllEntities()
    {
        var users = from user in _unitOfWork.Repository.Get() 
            select user; 
                

        return users.Any() ? Ok(users) : NoContent();
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
    public override IActionResult GetEntryById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = _unitOfWork.Repository.GetById(id);
        if (user is not null)
        {
            return Ok(user);
        }
        
        return NotFound($"User with ID: {id} not found");
    }
}