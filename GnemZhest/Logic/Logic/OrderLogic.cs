using DBLayer.Models;
using DBLayer.Providers;

namespace Logic.Logic;

public class OrderLogic : Logic<DBLayer.Models.Order>, IOrderLogic
{
    private readonly IOrderProvider provider;

    public OrderLogic(IOrderProvider provider) : base(provider) =>
        this.provider = provider;

    protected override async Task<bool> BeforeAddAsync(Order model)
    {
        if (string.IsNullOrEmpty(model.City) || string.IsNullOrEmpty(model.OrderAdress))
            return false;

        if (model.Ammount == 0)
            return false;

        if (await this.provider.GetAsync(model.Id) != null)
            return false;

        return true;
    }
}
