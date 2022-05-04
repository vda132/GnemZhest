using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Helpers;

public class JWTService : IJWTService
{
    private readonly IConfiguration configuration;

    public JWTService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string Generate(DBLayer.Models.User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("id", user.Id.ToString())
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
                  signingCredentials: signingCredentials,
                  claims: claims,
                  expires: DateTime.Now.AddYears(1));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["key"]);

        tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        }, out SecurityToken validatedToken);

        return (JwtSecurityToken)validatedToken;
    }
}
