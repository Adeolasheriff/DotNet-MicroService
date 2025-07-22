using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {

    }

    [HttpPost]
    public ActionResult TestInBoundConnection()
    {
        Console.WriteLine("Inbound test of command service.");
        return Ok("Inbound test of Platforms Controller successful.");
    }
  

}
