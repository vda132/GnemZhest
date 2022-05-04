using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Helpers;

public interface IJWTService
{
    string Generate(DBLayer.Models.User user);
    JwtSecurityToken Verify(string jwt);
}
