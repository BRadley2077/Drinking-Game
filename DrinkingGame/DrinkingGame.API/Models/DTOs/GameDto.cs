using DrinkingGame.API.Models.Domain;

namespace DrinkingGame.API.Models.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string GameName { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreatedById { get; set; }
    
    public User CreatedBy { get; set; }
}