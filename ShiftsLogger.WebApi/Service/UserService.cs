using Contracts;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Service.Contracts;
using Shared.Dto.User;
using Shared.Mappers;
using Shared.RequestFeatures;

namespace Service;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repository;

    public UserService(IRepositoryManager repository) =>
        _repository = repository;

    public async Task<(List<UserDto> users, PaginationMetaData metaData)> 
        GetAllUsersAsync(UserParameters userParameters, bool trackChanges)
    {
        var users = await _repository.User.GetAllUsersAsync(userParameters, trackChanges);

        var dtos = users.Select(u => u.MapToDto()).ToList();

        return (dtos, users.PaginationMetaData);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId, bool trackChanges)
    {
        var user = await TryGetUserEntity(userId, trackChanges);
        
        return user.MapToDto();
    }

    public async Task<UserDto> CreateUserAsync(UserForCreationDto user)
    {
        var entity = user.MapToEntity();
        
        _repository.User.CreateUser(entity);
        await _repository.SaveAsync();

        return entity.MapToDto();
    }

    public async Task DeleteUserAsync(Guid userId, bool trackChanges)
    {
        var entity = await TryGetUserEntity(userId, trackChanges);

        _repository.User.DeleteUser(entity);
        await _repository.SaveAsync();
    }

    public async Task UpdateUserAsync(Guid userId, UserForUpdateDto updateDto, bool trackChanges)
    {
        var entity  = await TryGetUserEntity(userId, trackChanges);

        entity.UpdateEntity(updateDto);
        await _repository.SaveAsync();
    }

    private async Task<User> TryGetUserEntity(Guid userId, bool trackChanges) =>
        (_repository.User.UserExists(userId)
            ? await _repository.User.GetUserByIdAsync(userId, trackChanges)
            : throw new UserNotFoundException(userId))!;
}