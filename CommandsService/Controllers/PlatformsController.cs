using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using OriApps.UniCommand.CommandsService.Data;
using OriApps.UniCommand.CommandsService.Data.DTO;

namespace OriApps.UniCommand.CommandsService.Controllers;

[Route("api/commands/[controller]")]
[ApiController]
public class PlatformsController(ICommandRepository repository, IMapper mapper) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PlatformReadDTO>>> GetPlatforms()
	{
		Console.WriteLine("Getting Platforms");
		
		var platforms = await repository.GetAllPlatformsAsync();
		
		return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
	}
}
