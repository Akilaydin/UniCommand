using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using OriApps.UniCommand.PlatformService.Data;
using OriApps.UniCommand.PlatformService.Data.DTO;
using OriApps.UniCommand.PlatformService.Models;
using OriApps.UniCommand.PlatformService.Services;

namespace OriApps.UniCommand.PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController(IPlatformRepository platformRepository, IMapper mapper, ICommandDataClient commandDataClient) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<PlatformReadDTO>>> GetPlatforms()
	{
		Console.WriteLine(nameof(GetPlatforms));
		
		var platforms = await platformRepository.GetAllPlatformsAsync();
		
		return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
	}
	
	[HttpGet("{id}", Name = nameof(GetPlatformById))]
	public async Task<ActionResult<PlatformReadDTO>> GetPlatformById(int id)
	{
		Console.WriteLine(nameof(GetPlatformById));
		
		var platform = await platformRepository.GetPlatformByIdAsync(id);

		if (platform == null)
		{
			return NotFound();
		}
		
		return Ok(mapper.Map<PlatformReadDTO>(platform));
	}
	
	[HttpPost]
	public async Task<ActionResult<PlatformReadDTO>> CreatePlatform([FromBody] PlatformCreateDTO platformDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var platform = mapper.Map<Platform>(platformDto);

		if (platform.Cost == null)
		{
			return NotFound($"CostType with ID {platformDto.CostTypeID} not found.");
		}

		await platformRepository.AddPlatformAsync(platform);
		await platformRepository.SaveChangesAsync();

		var readDto = mapper.Map<PlatformReadDTO>(platform);

		try
		{
			await commandDataClient.SendPlatformToCommand(readDto);
		} 
		catch (Exception e)
		{
			Console.WriteLine($"Error while sending platform to command: {e}");
		}
		
		return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, readDto);
	}
}
