namespace Logic.Logic;

public interface IAddableLogic<TModel> where TModel : DBLayer.Models.ModelBase
{
    Task<bool> AddAsync(TModel model);
}

