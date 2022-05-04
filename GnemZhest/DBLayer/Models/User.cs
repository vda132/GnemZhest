namespace DBLayer.Models;
public class User : ModelBase
{
    public string Name { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public IReadOnlyCollection<Cart> Carts { get; set; } = new List<Cart>();
}
