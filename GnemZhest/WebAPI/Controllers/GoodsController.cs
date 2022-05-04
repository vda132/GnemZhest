using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoodsController : BaseController<DBLayer.Models.Good>
{
    private readonly Logic.Logic.IGoodLogic logic;

    public GoodsController(Logic.Logic.IGoodLogic logic) : base(logic) =>
        this.logic = logic;
}
