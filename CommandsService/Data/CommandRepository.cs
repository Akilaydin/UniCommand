﻿using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Data;

public class CommandRepository(CommandsDbContext dbContext) : ICommandRepository
{
	public async Task<int> SaveChangesAsync()
	{
		return await dbContext.SaveChangesAsync();
	}

	public Task<List<Command>> GetAllCommandsAsync()
	{
		return dbContext.Commands.ToListAsync();
	}
	
	public Task<List<Platform>> GetAllPlatformsAsync()
	{
		return dbContext.Platforms.ToListAsync();
	}
	
	public Task<List<Command>> GetAllPlatformCommandsAsync(int platformId)
	{
		return dbContext.Commands.Where(x => x.Platform.Id == platformId).ToListAsync();
	}

	public Task<Command?> GetCommandByIdAsync(int platformId, int commandId)
	{
		return dbContext.Commands.FirstOrDefaultAsync(x => x.Platform.Id == platformId && x.Id == commandId);
	}

	public Task<bool> PlatformExistsAsync(int platformId)
	{
		return dbContext.Platforms.AnyAsync(x => x.Id == platformId);
	}

	public async Task CreateCommandAsync(int platformId, Command command)
	{
		ArgumentNullException.ThrowIfNull(command);

		var platform = await dbContext.Platforms.FirstOrDefaultAsync(x => x.Id == platformId);
		
		ArgumentNullException.ThrowIfNull(platform);

		command.Platform = platform;
		
		await dbContext.Commands.AddAsync(command);
	}

	public async Task CreatePlatformAsync(Platform platform)
	{
		ArgumentNullException.ThrowIfNull(platform);

		await dbContext.AddAsync(platform);
	}
}