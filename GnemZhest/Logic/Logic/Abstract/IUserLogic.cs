using Logic.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace Logic.Logic;

public interface IUserLogic :IBaseLogic<DBLayer.Models.User>
{
    Task<RegistrationResultDTO> RegisterAsync(UserRegisterDTO dto);
    Task<DBLayer.Models.User?> LoginAsync(UserLoginDTO dto);
    Task<UserAuthorizedDTO> GetAuthorizedUser(JwtSecurityToken jwt);
}

