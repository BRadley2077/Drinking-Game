using AutoMapper;
using DrinkingGame.API.Data;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DrinkingGame.API.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<UserDto> CreateAsync(AddUserRequestDto requestDto);
    Task<UserDto> GetById(Guid id);
    Task<UserDto> UpdateAsync(Guid userId, UpdateUserRequestDto requestDto);
    Task<bool> DeleteAsync(Guid id);
}

public class UserRepository : IUserRepository
{
    private readonly DrinkingGameDbContext _dbContext;
    private IMapper _mapper;

    public UserRepository(DrinkingGameDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
    
    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        
        return user == null ? new UserDto() : _mapper.Map<UserDto>(user);
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

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(AddUserRequestDto requestDto)
    {
        var userDomainModel = _mapper.Map<User>(requestDto);

        await _dbContext.Users.AddAsync(userDomainModel);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<UserDto>(userDomainModel);
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