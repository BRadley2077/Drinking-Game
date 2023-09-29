using DrinkingGame.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DrinkingGame.API.Data;

public class DrinkingGameDbContext : DbContext
{
    public DrinkingGameDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Rule> Rules { get; set; }
}