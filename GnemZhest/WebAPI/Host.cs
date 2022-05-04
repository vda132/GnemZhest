using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Helpers;

namespace WebAPI;

public class Host : Logic.Host
{
    private bool InitializeConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        if (string.IsNullOrWhiteSpace(this.JsonConfigPath))
            throw new Exception("JSON config path is empty.");

        if (!File.Exists(this.JsonConfigPath))
            throw new Exception($"JSON config path does not exist: {this.JsonConfigPath}");

        try
        {
            using var test = File.OpenRead(this.JsonConfigPath);
            configurationBuilder.AddJsonFile(this.JsonConfigPath);
        }
        catch (FileNotFoundException)
        {
            throw new Exception($"JSON config path exists but is not readable: {JsonConfigPath}");
        }

        this.configuration = configurationBuilder.Build();

        return true;
    }

    public override bool ConfigureServices(IServiceCollection serviceCollection)
    {
        this.InitializeConfiguration();

        serviceCollection.AddSingleton<IJWTService, JWTService>();

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<string>("Key")));

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return base.ConfigureServices(serviceCollection);
    }
}
