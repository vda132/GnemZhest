namespace DBLayer.Models;

public class Good : ModelBase
{
    public string Name { get; set; } = string.Empty;
    
    public string? Image1 { get; set; } = string.Empty;

    public string? Image2 { get; set; } = string.Empty;

    public string? Image3 { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsAvaliable { get; set; }
    public IReadOnlyCollection<Order> Orders { get; set; } = new List<Order>();
}
