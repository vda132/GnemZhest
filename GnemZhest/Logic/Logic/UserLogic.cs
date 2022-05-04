using DBLayer.Models;
using DBLayer.Providers;
using Logic.DTOs;
using Logic.PasswordHasher;
using System.IdentityModel.Tokens.Jwt;

namespace Logic.Logic;

internal class UserLogic : Logic<DBLayer.Models.User>, IUserLogic
{
    private readonly IUserProvider provider;
    private readonly IPasswordHasher passwordHasher;

    public UserLogic(IUserProvider provider, IPasswordHasher passwordHasher) : base(provider)
    {
        this.provider = provider;
        this.passwordHasher = passwordHasher;
    }

    public async Task<User?> LoginAsync(UserLoginDTO dto)
    {
        if(!ValidateUserLoginDTO(dto))
            return null;

        var user = await this.provider.GetByLoginAsync(dto.Login);

        if(user is null)
            return null;

        var password = passwordHasher.HashPassword(dto.Password);

        if(!password.Equals(user.Password))
            return null;
        
        return user;
    }

    public async Task<RegistrationResultDTO> RegisterAsync(UserRegisterDTO dto)
    {
        if (dto is null)
            return new RegistrationResultDTO
            {
                Status = 500,
                Message = "Please input all fields"
            };

        var user = await this.provider.GetByLoginAsync(dto.Login);

        if (user is not null)
            return new RegistrationResultDTO
            {
                Status = 500,
                Message = "The login is already exists"
            };

        var password = passwordHasher.HashPassword(dto.Password);
        dto.Password = password;
        var userModel = this.GetUserFromDTO(dto);
        
        if (await this.provider.AddAsync(userModel))
            return new RegistrationResultDTO
            {
                Status = 200,
                Message = "Ok"
            };

        return new RegistrationResultDTO
        {
            Status = 500,
            Message = "Something went wrong"
        };
    }

    public async Task<UserAuthorizedDTO> GetAuthorizedUser(JwtSecurityToken jwt)
    {
        var id = int.Parse(jwt.Claims.FirstOrDefault(x => x.Type.Equals("id"))!.Value);

        var user = await this.provider.GetAsync(id);

        var authorizedUserDTO = new UserAuthorizedDTO
        {
            Id = user!.Id,
            Name = user!.Name,
            SurName = user!.SurName,
            Email = user!.Email,
            Login = user!.Login,
            Phone = user!.Phone,
            Role = user!.Role
        };

        return authorizedUserDTO;
    }

    private bool ValidateUserLoginDTO(UserLoginDTO dto)
    {
        if (dto is null)
            return false;

        if (string.IsNullOrEmpty(dto.Login) ||
            string.IsNullOrEmpty(dto.Password))
            return false;

        return true;
    }

    private User GetUserFromDTO(UserRegisterDTO dto)
    {
        var user = new User
        {
            Name = dto.Name,
            SurName = dto.SurName,
            Phone = dto.Phone,
            Email = dto.Email,
            Role = dto.Role,
            Login = dto.Login,
            Password = dto.Password
        };

        return user;
    }
}
