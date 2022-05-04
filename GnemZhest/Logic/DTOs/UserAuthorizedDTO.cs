namespace Logic.DTOs;

public class UserAuthorizedDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
