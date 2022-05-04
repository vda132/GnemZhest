using DBLayer.Providers;

namespace Logic.Logic;

public class OrderLogic : Logic<DBLayer.Models.Order>, IOrderLogic
{
    private readonly IOrderProvider provider;

    public OrderLogic(IOrderProvider provider) : base(provider) =>
        this.provider = provider;
}
