namespace DBLayer.Providers;

public interface IGetableProvider<TModel> where TModel : Models.ModelBase
{
    Task<TModel?> GetAsync(int id);
    Task<IReadOnlyCollection<TModel>?> GetAllAsync();

}

