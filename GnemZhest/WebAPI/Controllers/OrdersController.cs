using DBLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController<DBLayer.Models.Order>
{
    private readonly Logic.Logic.IOrderLogic logic;

    public OrdersController(Logic.Logic.IOrderLogic logic) : base(logic) =>
        this.logic = logic;

    [HttpPost]
    public override async Task<JsonResult> Post([FromBody] Order model)
    {
        var result = await this.logic.AddAsync(model);

        return new JsonResult(result);
    }
}
