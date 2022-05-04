namespace DBLayer.Providers;

public interface IAddableProvider<TModel> where TModel : Models.ModelBase
{
    Task<bool> AddAsync(TModel model);
}

