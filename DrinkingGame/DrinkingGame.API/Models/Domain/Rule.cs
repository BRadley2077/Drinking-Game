namespace DrinkingGame.API.Models.Domain;

public class Rule
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Action { get; set; }
    public Guid GameId { get; set; }

    public Game Game { get; set; }
}