namespace DBLayer.Providers;

public interface IDeleteableProvider
{
    Task<bool> DeleteAsync(int id);
}
