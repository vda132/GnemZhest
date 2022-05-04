namespace Logic.Logic;

public interface IUpdateableLogic<TModel> where TModel : DBLayer.Models.ModelBase
{
    Task<bool> UpdateAsync(int id, TModel model);
}
