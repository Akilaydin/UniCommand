using OriApps.UniCommand.PlatformService.Models;

namespace OriApps.UniCommand.PlatformService.Data;

public interface IPlatformRepository
{
	Task<int> SaveChangesAsync();
	
	Task<List<Platform>> GetAllPlatformsAsync();
	
	Task<Platform?> GetPlatformByIdAsync(int id);
	
	Task AddPlatformAsync(Platform platform);
}