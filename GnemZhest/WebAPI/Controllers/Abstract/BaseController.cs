using Logic.Logic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController<TModel> : ControllerBase where TModel : DBLayer.Models.ModelBase
{
    private readonly IBaseLogic<TModel> logic;

    public BaseController(IBaseLogic<TModel> logic) =>
        this.logic = logic;

    [HttpPost]
    public virtual async Task<JsonResult> Post([FromBody] TModel model)
    {
        var result = await this.logic.AddAsync(model);

        return new JsonResult(result);
    }

    [HttpPut("{id}")]
    public virtual async Task<JsonResult> Put(int id, [FromBody] TModel model)
    {
        var result = "Updated sucessfully";

        if (await this.logic.UpdateAsync(id, model))
            return new JsonResult(result);

        result = "failed";

        return new JsonResult(result);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var result = "Deleted sucessfully";

        if (await this.logic.DeleteAsync(id))
            return new JsonResult(result);

        result = "Failed";

        return new JsonResult(result);
    }

    [HttpGet("{id}")]
    public virtual async Task<JsonResult> Get(int id) =>
        new JsonResult(await this.logic.GetAsync(id));

    [HttpGet]
    public virtual async Task<JsonResult> GetAll() =>
        new JsonResult((await this.logic.GetAllAsync())!.ToList());
}
