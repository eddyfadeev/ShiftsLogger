using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Application.Interfaces.Services;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();

        return users.Count == 0 ? NoContent() : Ok(users);
    }

    [HttpGet("{id:int}")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
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

    [HttpDelete("{id:int}")]
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