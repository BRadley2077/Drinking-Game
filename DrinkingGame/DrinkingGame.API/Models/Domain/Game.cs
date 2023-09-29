using System.Data.Common;

namespace DrinkingGame.API.Models.Domain;

public class Game
{
    public Guid Id { get; set; }
    public string GameName { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreatedById { get; set; }
    
    public User CreatedBy { get; set; }
}