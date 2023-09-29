namespace DrinkingGame.API.Models.DTOs;

public class UpdateGameRequestDto
{
    public string GameName { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreatedById { get; set; }
}