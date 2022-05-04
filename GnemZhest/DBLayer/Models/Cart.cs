namespace DBLayer.Models;
public class Cart : ModelBase
{
    public int? UserId { get; set; }
    public int? GoodId { get; set; }
    public int Quantity { get; set; }
    public bool IsOrdered { get; set; }
    public virtual Good? Good { get; set; }
    public virtual User? User { get; set; }
    public virtual Order? Order { get; set; }
}

