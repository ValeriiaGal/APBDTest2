using Microsoft.AspNetCore.Mvc;
using Services;

namespace API;

[ApiController]
public class Controller(IService service) : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var drivers = await service.Get();
            return Ok(drivers);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}