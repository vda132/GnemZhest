using Logic.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<DBLayer.Models.User>
    {
        private readonly Logic.Logic.IUserLogic logic;
        private readonly IJWTService jwtService;

        public UsersController(Logic.Logic.IUserLogic logic, IJWTService jwtService) : base(logic)
        { 
            this.logic = logic;
            this.jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<JsonResult> Register([FromBody] UserRegisterDTO dto) =>
            new JsonResult(await this.logic.RegisterAsync(dto));

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] Logic.DTOs.UserLoginDTO dto)
        {
            var user = await this.logic.LoginAsync(dto);

            if (user is null)
                return new JsonResult(new TokenDTO
                {
                    Status = 500,
                    Token = string.Empty
                });

            var jwt = jwtService.Generate(user);

            return new JsonResult(new TokenDTO 
            { 
                Status = 200,
                Token = jwt
            });
        }
    }
}
