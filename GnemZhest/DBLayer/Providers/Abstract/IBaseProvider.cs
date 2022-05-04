namespace DBLayer.Providers;

public interface IBaseProvider<TModel> :
    IGetableProvider<TModel>,
    IAddableProvider<TModel>,
    IUpdateableProvider<TModel>,
    IDeleteableProvider
    where TModel : Models.ModelBase
{
}

