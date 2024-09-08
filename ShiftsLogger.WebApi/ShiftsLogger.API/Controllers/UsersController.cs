using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

/// <summary>
/// Handles operations related to Users.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves a list of all users in the system.
    /// </summary>
    /// <returns>An IActionResult containing a list of User objects if any exist; otherwise, returns a NoContent result.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();

        return users.Count == 0 ? NoContent() : Ok(users);
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
    public IActionResult GetUserById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = _userService.GetUser(id);
        if (user is not null)
        {
            return Ok(user);
        }
        
        return NotFound($"User with ID: {id} not found");
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="user">The user object containing the details of the user to be added.</param>
    /// <returns>An IActionResult indicating success or failure. If successful, returns a 201 Created response with the user object; otherwise, returns a 400 BadRequest response.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _userService.AddUser(user);
        if (result > 0)
        {
            return CreatedAtAction(
                nameof(GetUserById), 
                new { id = user.Id }, 
                user
                );
        }
        
        return BadRequest("Failed to add user");
    }

    /// <summary>
    /// Updates an existing user with the given details.
    /// </summary>
    /// <param name="id">The ID of the user to be updated.</param>
    /// <param name="user">The user details to update.</param>
    /// <returns>An IActionResult indicating the outcome of the operation: OK if successful or BadRequest if validation fails or the update is unsuccessful.</returns>
    [HttpPut("{id:int}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest("User ID does not match");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = _userService.UpdateUser(user);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to update user");
    }

    /// <summary>
    /// Deletes a user from the system based on the provided user ID.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <returns>An IActionResult indicating success with StatusCode 200 OK,
    /// or a BadRequest result if the operation fails, or a NotFound result if no user is found with the provided ID.</returns>
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteUser(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = _userService.GetUser(id);
        if (user is null)
        {
            return NotFound($"User with ID: {id} not found");
        }
        
        var result = _userService.RemoveUser(user);
        if (result > 0)
        {
            return Ok();
        }
        
        return BadRequest("Failed to remove user");
    }
}