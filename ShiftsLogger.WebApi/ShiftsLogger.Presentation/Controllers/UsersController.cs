using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Service.Contracts;
using Shared.Dto.User;
using Shared.RequestFeatures;
using ShiftsLogger.Presentation.ActionFilters;
using ShiftsLogger.Presentation.Extensions;

namespace ShiftsLogger.Presentation.Controllers;

[Route("api/users")]
[ApiController]
[OutputCache(PolicyName = "15MinExpiry")]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;

    public UsersController(IServiceManager service) =>
        _service = service;

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
    {
        var pagedResult = await _service.UserService
            .GetAllUsersAsync(userParameters, trackChanges: false);

        this.SetPaginationMetadata(pagedResult.metaData);

        return Ok(pagedResult.users);
    }

    [HttpGet("{userId:guid}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _service.UserService.GetUserByIdAsync(userId, trackChanges: false);

        return Ok(user);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto user)
    {
        var createdUser = await _service.UserService.CreateUserAsync(user);

        return CreatedAtRoute
        (
            "GetUserById",
            new { userId = createdUser.Id },
            createdUser
        );
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _service.UserService.DeleteUserAsync(userId, trackChanges: false);

        return NoContent();
    }
    
    [HttpPut("{userId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserForUpdateDto user)
    {
        await _service.UserService.UpdateUserAsync(userId, user, trackChanges: true);  
        
        return NoContent();
    }
}