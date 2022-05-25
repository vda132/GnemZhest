using DBLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Providers;

public class OrderProvider : Provider, IOrderProvider
{
    public OrderProvider(IConfiguration configuration) : base(configuration) { }

    public OrderProvider(string connectionString) : base(connectionString) { }

    public async Task<bool> AddAsync(Order model)
    {
        if (!await ValidateOrderAsync(model))
            return false;

        await using var context = this.ZhestContext;
        await context.Orders.AddAsync(model);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var context = this.ZhestContext;

        var order = await GetOrderAsync(id, context);

        if (order is null)
            return false;

        context.Orders.Remove(order);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyCollection<Order>?> GetAllAsync()
    {
        await using var context = this.ZhestContext;
        return await context.Orders.ToListAsync();    
    }

    public async Task<Order?> GetAsync(int id)
    {
        await using var context = this.ZhestContext;
        return await GetOrderAsync(id, context);
    }

    public async Task<bool> UpdateAsync(int id, Order model)
    {
        await using var context = this.ZhestContext;

        var order = await GetOrderAsync(id, context);

        if (order is null)
            return false;

        order.OrderAdress = model.OrderAdress;
        order.Price = model.Price;
        order.UserId = model.UserId;
        order.GoodId = model.GoodId;
        order.City = model.City;

        context.Orders.Update(order);
        await context.SaveChangesAsync();

        return true;
    }

    private static async Task<Order?> GetOrderAsync(int id, ZhestContext zhestContext) =>
        await zhestContext.Orders.FindAsync(id);

    private async Task<bool> ValidateOrderAsync(Order model)
    {
        if (model is null)
            return false;

        if (string.IsNullOrEmpty(model.OrderAdress) ||
            string.IsNullOrEmpty(model.City))
            return false;

        await using var context = this.ZhestContext;

        if (await context.Orders.FindAsync(model.Id) is not null)
            return false;
        
        if (await context.Goods.FindAsync(model.GoodId) is null)
            return false;
          
        return true;
    }
}

