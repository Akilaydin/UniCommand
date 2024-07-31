using Microsoft.AspNetCore.Mvc;

namespace OriApps.UniCommand.CommandsService.Controllers;

[Route("api/commands/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
	[HttpPost]
	public ActionResult TestInboundConnection()
	{
		Console.WriteLine("TestInboundConnection");
		
		return Ok("InboundConnection successful");
	}
}
