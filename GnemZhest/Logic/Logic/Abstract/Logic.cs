using DBLayer.Models;
using DBLayer.Providers;

namespace Logic.Logic;

public class Logic<TModel> where TModel : ModelBase
{
    private readonly IBaseProvider<TModel> provider;

    public Logic(IBaseProvider<TModel> provider) =>
        this.provider = provider;

    protected virtual Task<bool> BeforeAddAsync(TModel model) =>
        Task.FromResult(true);

    public virtual async Task<bool> AddAsync(TModel model)
    {
        if(await this.BeforeAddAsync(model))
            return await this.provider.AddAsync(model);
        
        return false;
    }

    protected virtual Task<bool> BeforeUpdateAsync(TModel model) =>
        Task.FromResult(true);

    public virtual async Task<bool> UpdateAsync(int id, TModel model)
    {
        if (await this.BeforeUpdateAsync(model))
            return await this.provider.UpdateAsync(id, model);

        return false;
    }

    public virtual async Task<bool> DeleteAsync(int id) =>
        await this.provider.DeleteAsync(id);

    public virtual async Task<IReadOnlyCollection<TModel>?> GetAllAsync() =>
        await this.provider.GetAllAsync();

    public virtual async Task<TModel?> GetAsync(int id) =>
        await this.provider.GetAsync(id);
}

