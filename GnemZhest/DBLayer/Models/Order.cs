namespace DBLayer.Models;
public class Order : ModelBase
{
    public string OrderAdress { get; set; } = string.Empty;
    public bool IsSend { get; set; }
    public int CartId { get; set; }
    public string? City { get; set; } = string.Empty;
    public Cart? Cart { get; set; }
}

