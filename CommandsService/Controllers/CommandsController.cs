using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using OriApps.UniCommand.CommandsService.Data;
using OriApps.UniCommand.CommandsService.Data.DTO;
using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController(ICommandRepository commandRepository, IMapper mapper) : ControllerBase
{
	[HttpPost("platforms/{platformId:int}/commands", Name = nameof(CreateCommand))]
	public async Task<ActionResult> CreateCommand(int platformId, CommandCreateDTO commandCreateDTO)
	{
		Console.WriteLine(nameof(CreateCommand));

		var command = mapper.Map<CommandCreateDTO, Command>(commandCreateDTO);
		
		await commandRepository.CreateCommandAsync(platformId, command);
		
		await commandRepository.SaveChangesAsync();
		
		var readDTO = mapper.Map<Command, CommandReadDTO>(command);

		return CreatedAtRoute(nameof(GetPlatformCommands), new { platformId = platformId, commandId = readDTO.Id },readDTO);
	}

	[HttpGet("platforms/{platformId:int}/commands/{commandId:int}", Name = nameof(GetCommandById))]
	public async Task<ActionResult<CommandReadDTO>> GetCommandById(int platformId, int commandId)
	{
		Console.WriteLine(nameof(GetCommandById) + " " + platformId + " " + commandId);

		if (await commandRepository.PlatformExistsAsync(platformId) == false)
		{
			return NotFound();
		}

		var command = await commandRepository.GetCommandByIdAsync(platformId, commandId);

		if (command == null)
		{
			return NotFound();
		}
		
		var readDTO = mapper.Map<Command, CommandReadDTO>(command);
		
		return Ok(readDTO);
	}

	[HttpGet("platforms/commands", Name = nameof(GetCommands))]
	public async Task<ActionResult<IEnumerable<CommandReadDTO>>> GetCommands()
	{
		Console.WriteLine(nameof(GetCommands));

		var commands = await commandRepository.GetAllCommandsAsync();
		
		return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));
	}
	
	[HttpGet("platforms/{platformId:int}/commands", Name = nameof(GetPlatformCommands))]
	public async Task<ActionResult<IEnumerable<CommandReadDTO>>> GetPlatformCommands(int platformId)
	{
		Console.WriteLine(nameof(GetPlatformCommands) + " " + platformId);

		if (await commandRepository.PlatformExistsAsync(platformId) == false)
		{
			return NotFound();
		}
		
		var commands = await commandRepository.GetAllPlatformCommandsAsync(platformId);
		
		return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));
	}
}
