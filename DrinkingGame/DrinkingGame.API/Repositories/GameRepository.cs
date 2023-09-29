using DrinkingGame.API.Data;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DrinkingGame.API.Repositories;

public interface IGameRepository
{ 
    Task<List<Game>> GetAllAsync();
    Task<Game> CreateAsync(Game game);
}

public class GameRepository : IGameRepository
{
    private readonly DrinkingGameDbContext _drinkingGameDbContext;

    public GameRepository(DrinkingGameDbContext drinkingGameDbContext)
    {
        _drinkingGameDbContext = drinkingGameDbContext;
    }

    public async Task<List<Game>> GetAllAsync()
    {
        return await _drinkingGameDbContext.Games.Include("CreatedBy").ToListAsync();
    }

    public async Task<Game> CreateAsync(Game game)
    {
        await _drinkingGameDbContext.Games.AddAsync(game);
        await _drinkingGameDbContext.SaveChangesAsync();
        return game;
    }
}