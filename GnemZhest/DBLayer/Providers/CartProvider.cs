using DBLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Providers;

public class CartProvider : Provider, ICartProvider
{
    public CartProvider(IConfiguration configuration) : base(configuration) { }
    public CartProvider(string connectionString) : base(connectionString) { }

    public async Task<bool> AddAsync(Cart model)
    {
        if (!await this.ValidateCartAsync(model))
            return false;

        await using var context = this.ZhestContext;
        await context.Carts.AddAsync(model);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var context = this.ZhestContext;

        var cart = await GetCartAsync(id, context);

        if (cart is null)
            return false;

        context.Carts.Remove(cart);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyCollection<Cart>?> GetAllAsync()
    {
        await using var context = this.ZhestContext;
        return await context.Carts.ToListAsync();
    }

    public async Task<Cart?> GetAsync(int id)
    {
        await using var context = this.ZhestContext;
        return await GetCartAsync(id, context);
    }

    public async Task<bool> UpdateAsync(int id, Cart model)
    {
        await using var context = this.ZhestContext;

        var cart = await GetCartAsync(id, context);

        if (cart is null)
            return false;

        cart.GoodId = model.GoodId;
        cart.UserId = model.UserId;
        cart.Quantity = model.Quantity;
        cart.IsOrdered = model.IsOrdered;

        context.Carts.Update(cart);
        await context.SaveChangesAsync();

        return true;
    }

    private static async Task<Cart?> GetCartAsync(int id, ZhestContext zhestContext) =>
        await zhestContext.Carts.FindAsync(id);

    private async Task<bool> ValidateCartAsync(Cart model)
    {
        if (model is null)
            return false;

        if (model.Quantity == 0 ||
            model.IsOrdered ||
            model.UserId == 0 ||
            model.GoodId == 0)
            return false;

        await using var context = this.ZhestContext;

        if (await context.Carts.FindAsync(model.Id) is not null)
            return false;
  
        if (await context.Users.FindAsync(model.UserId) is null)
            return false;

        if (await context.Goods.FindAsync(model.GoodId) is null)
            return false;

        return true;
    }
}

