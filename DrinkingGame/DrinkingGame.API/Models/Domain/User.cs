namespace DrinkingGame.API.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserImageUrl { get; set; }
}