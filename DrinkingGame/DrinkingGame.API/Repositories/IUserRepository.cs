using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;

namespace DrinkingGame.API.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<UserDto> CreateAsync(AddUserRequestDto requestDto);
    Task<UserDto> GetById(Guid id);
    Task<UserDto> UpdateAsync(Guid userId, UpdateUserRequestDto requestDto);
    Task<bool> DeleteAsync(Guid id);
}