using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Data;

public interface ICommandRepository
{
	Task<int> SaveChangesAsync();
	
	Task<List<Command>> GetAllCommandsAsync();
	Task<List<Platform>> GetAllPlatformsAsync();
	Task<List<Command>> GetAllPlatformCommandsAsync(int platformId);
	
	Task<Command?> GetCommandByIdAsync(int platformId, int commandId);
	Task<bool> PlatformExistsAsync(int platformId);
	
	Task CreateCommandAsync(int platformId, Command command);
	Task CreatePlatformAsync(Platform platform);
}