namespace DBLayer.Providers;

public interface IUpdateableProvider<TModel> where TModel : Models.ModelBase
{
    Task<bool> UpdateAsync(int id, TModel model);
}

