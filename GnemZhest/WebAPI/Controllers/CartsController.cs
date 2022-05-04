using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : BaseController<DBLayer.Models.Cart>
{
    private readonly Logic.Logic.ICartLogic logic; 

    public CartsController(Logic.Logic.ICartLogic logic) : base(logic) =>
        this.logic = logic;
}
