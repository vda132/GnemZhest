namespace DBLayer.Models;
public class Order : ModelBase
{
    public int UserId { get; set; }
    public int GoodId { get; set; }
    public string? City { get; set; } = string.Empty;
    public string OrderAdress { get; set; } = string.Empty;
    public int Ammount { get; set; }
    public decimal Price { get; set; }
    public Good? Good { get; set; }
    public User? User { get; set; }
}

