namespace Logic.Logic;

public interface IGetableLogic<TModel> where TModel : DBLayer.Models.ModelBase
{
    Task<TModel?> GetAsync(int id);
    Task<IReadOnlyCollection<TModel>?> GetAllAsync();
}

