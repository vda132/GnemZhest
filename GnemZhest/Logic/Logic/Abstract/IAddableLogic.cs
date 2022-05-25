namespace Logic.Logic;

public interface IAddableLogic<TModel> where TModel : DBLayer.Models.ModelBase
{
    Task<DTOs.ResultDTO> AddAsync(TModel model);
}

