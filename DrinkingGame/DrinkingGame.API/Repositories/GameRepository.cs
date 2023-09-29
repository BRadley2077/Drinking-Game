using DrinkingGame.API.Data;
using DrinkingGame.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DrinkingGame.API.Repositories;

public interface IGameRepository
{ Task<List<Game>> GetAllAsync();
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
        return await _drinkingGameDbContext.Games.ToListAsync();
    }
}