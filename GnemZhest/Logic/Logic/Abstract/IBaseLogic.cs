namespace Logic.Logic;
public interface IBaseLogic<TModel> :
    IAddableLogic<TModel>,
    IUpdateableLogic<TModel>,
    IDeleteableLogic,
    IGetableLogic<TModel>
    where TModel : DBLayer.Models.ModelBase
{
}
