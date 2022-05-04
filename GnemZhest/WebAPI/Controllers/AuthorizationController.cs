using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IJWTService jwtService;
    private readonly Logic.Logic.IUserLogic logic;

    public AuthorizationController(IJWTService jwtService, Logic.Logic.IUserLogic userLogic)
    { 
        this.jwtService = jwtService;
        this.logic = userLogic;
    }

    [HttpGet("auth/me")]
    public async Task<JsonResult> AuthMe()
    {
        var token = HttpContext.GetTokenAsync("access_token").Result;

        var jwt = jwtService.Verify(token!);

        var user = await logic.GetAuthorizedUser(jwt);

        return new JsonResult(user);
    }
}
