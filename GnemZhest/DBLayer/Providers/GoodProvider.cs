using DBLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Providers;

public class GoodProvider : Provider, IGoodProvider
{
    public GoodProvider(IConfiguration configuration) : base(configuration) { }

    public GoodProvider(string connectionString) : base(connectionString) { }

    public async Task<bool> AddAsync(Good model)
    {
        if (!await ValidateGoodAsync(model))
            return false;

        await using var context = this.ZhestContext;
        await context.Goods.AddAsync(model);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var context = this.ZhestContext;

        var good = await GetGoodAsync(id, context);

        if (good is null)
            return false;

        context.Goods.Remove(good);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyCollection<Good>?> GetAllAsync()
    {
        await using var context = this.ZhestContext;

        var goods = await context.Goods.ToListAsync();

        return goods;
    }

    public async Task<Good?> GetAsync(int id)
    {
        await using var context = this.ZhestContext;
        return await GetGoodAsync(id, context); 
    }

    public async Task<bool> UpdateAsync(int id, Good model)
    {
        await using var context = this.ZhestContext;

        var good = await GetGoodAsync(id, context);

        if (good is null)
            return false;

        good.Name = model.Name;
        good.Price = model.Price;
        good.IsAvaliable = model.IsAvaliable;

        context.Goods.Update(good);
        await context.SaveChangesAsync();

        return true;
    }

    private static async Task<Good?> GetGoodAsync(int id, ZhestContext zhestContext)
    {
        var good = await zhestContext.Goods.FindAsync(id);
        return good;
    }

    private async Task<bool> ValidateGoodAsync(Good model)
    {
        if (model is null)
            return false;

        if (string.IsNullOrEmpty(model.Name) ||
            model.Price == 0 ||
            !model.IsAvaliable)
            return false;

        await using var context = this.ZhestContext;

        if (await context.Goods.FindAsync(model.Id) != null)
            return false;

        return true;
    }
}
