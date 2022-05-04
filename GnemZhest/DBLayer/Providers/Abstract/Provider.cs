using Microsoft.Extensions.Configuration;

namespace DBLayer.Providers;

public class Provider
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public Provider(IConfiguration configuration) =>
        this.configuration = configuration;

    public Provider(string connectionString) =>
        this.connectionString = connectionString;

    protected ZhestContext ZhestContext =>
        new(this.connectionString is null
            ? this.configuration["ConnectionStrings:DefaultConnectionVadim"]
            : this.connectionString);
}

