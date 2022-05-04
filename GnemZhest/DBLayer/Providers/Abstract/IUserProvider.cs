namespace DBLayer.Providers;

public interface IUserProvider : IBaseProvider<Models.User>
{
    Task<Models.User?> GetByLoginAsync(string login);
}

