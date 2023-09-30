using AutoMapper;
using DrinkingGame.API.Data;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DrinkingGame.API.Repositories;

public interface IGameRepository
{ 
    Task<List<Game>> GetAllAsync();
    Task<GameDto> GetById(Guid id);
    Task<GameDto> UpdateAsync(Guid id, UpdateGameRequestDto requestDto);
    Task<Game> CreateAsync(Game game);
    Task<bool> DeleteAsync(Guid id);
}

public class GameRepository : IGameRepository
{
    private readonly DrinkingGameDbContext _drinkingGameDbContext;
    private readonly IMapper _mapper;

    public GameRepository(DrinkingGameDbContext drinkingGameDbContext, IMapper mapper)
    {
        _drinkingGameDbContext = drinkingGameDbContext;
        _mapper = mapper;
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _drinkingGameDbContext.Games.Include("CreatedBy").ToListAsync();
    }

    public async Task<GameDto> GetById(Guid id)
    {
        var game = await _drinkingGameDbContext.Games
            .Include("CreatedBy")
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return game == null ? new GameDto() : _mapper.Map<GameDto>(game);
    }

    public async Task<GameDto> UpdateAsync(Guid id, UpdateGameRequestDto requestDto)
    {
        var game = await _drinkingGameDbContext.Games.FirstOrDefaultAsync(x => x.Id == id);

        if (game == null)
        {
            return new GameDto();
        }

        game.GameName = requestDto.GameName;
        game.CreateDate = requestDto.CreateDate;
        game.CreatedById = requestDto.CreatedById;
        
        await _drinkingGameDbContext.SaveChangesAsync();

        return _mapper.Map<GameDto>(game);
    }

    public async Task<Game> CreateAsync(Game game)
    {
        await _drinkingGameDbContext.Games.AddAsync(game);
        await _drinkingGameDbContext.SaveChangesAsync();
        return game;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var game = await _drinkingGameDbContext.Games.FirstOrDefaultAsync(x => x.Id == id);

        if (game == null)
        {
            return false;
        }

        _drinkingGameDbContext.Remove(game);
        await _drinkingGameDbContext.SaveChangesAsync();

        return true;
    }
}