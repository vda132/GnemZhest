using DBLayer.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBLayer;

public abstract class Host
{
    protected IConfiguration configuration;
    protected string JsonConfigPath = "D:\\GnemZhest\\GnemZhest\\DBLayer\\config.json";

    public virtual bool ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions();

        serviceCollection.AddSingleton(this.configuration);

        serviceCollection.AddSingleton<ICartProvider,  CartProvider>();
        serviceCollection.AddSingleton<IGoodProvider, GoodProvider>();
        serviceCollection.AddSingleton<IOrderProvider, OrderProvider>();
        serviceCollection.AddSingleton<IUserProvider, UserProvider>();

        return true;
    }
}

