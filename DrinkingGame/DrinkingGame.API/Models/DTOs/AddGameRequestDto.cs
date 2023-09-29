namespace DrinkingGame.API.Models.DTOs;

public class AddGameRequestDto
{
    public string GameName { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreatedById { get; set; }
}