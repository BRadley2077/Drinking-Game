namespace DrinkingGame.API.Models.DTOs;

public class AddUserRequestDto
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserImageUrl { get; set; }
}