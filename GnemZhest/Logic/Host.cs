using Logic.Logic;
using Logic.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

public class Host : DBLayer.Host
{
    public override bool ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IGoodLogic, GoodLogic>();
        serviceCollection.AddSingleton<IOrderLogic, OrderLogic>();
        serviceCollection.AddSingleton<IUserLogic, UserLogic>();
        serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher.PasswordHasher>();

        return base.ConfigureServices(serviceCollection);
    }
}
