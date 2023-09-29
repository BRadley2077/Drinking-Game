using DrinkingGame.API.Data;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DrinkingGame.API.Repositories;

public class SQLUserRepository : IUserRepository
{
    private readonly DrinkingGameDbContext _dbContext;

    public SQLUserRepository(DrinkingGameDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
    
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        
        if (user == null)
        {
            return new UserDto();
        }

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserImageUrl = user.UserImageUrl
        };
    }

    public async Task<UserDto> UpdateAsync(Guid userId, UpdateUserRequestDto requestDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return new UserDto();
        }

        user.UserName = requestDto.UserName;
        user.FirstName = requestDto.FirstName;
        user.LastName = requestDto.LastName;
        user.UserImageUrl = requestDto.UserImageUrl;

        await _dbContext.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserImageUrl = user.UserImageUrl
        };
    }

    public async Task<UserDto> CreateAsync(AddUserRequestDto requestDto)
    {
        var userDomainModel = new User
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            UserName = requestDto.UserName,
            UserImageUrl = requestDto.UserImageUrl
        };

        await _dbContext.Users.AddAsync(userDomainModel);
        await _dbContext.SaveChangesAsync();

        return new UserDto
        {
            Id = userDomainModel.Id,
            FirstName = userDomainModel.FirstName,
            LastName = userDomainModel.LastName,
            UserName = userDomainModel.UserName,
            UserImageUrl = userDomainModel.UserImageUrl
        };
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return false;
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}