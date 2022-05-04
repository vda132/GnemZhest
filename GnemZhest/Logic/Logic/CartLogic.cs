using DBLayer.Providers;

namespace Logic.Logic;

public class CartLogic : Logic<DBLayer.Models.Cart>, ICartLogic
{
    private readonly ICartProvider provider;

    public CartLogic(ICartProvider provider) : base(provider) =>
        this.provider = provider;
}

