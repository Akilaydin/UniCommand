using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Models;

namespace OriApps.UniCommand.PlatformService.Data;

public class PlatformRepository(PlatformServiceDbContext dbContext) : IPlatformRepository
{
	public Task<int> SaveChangesAsync()
	{
		return dbContext.SaveChangesAsync();
	}

	public Task<List<Platform>> GetAllPlatformsAsync()
	{
		return dbContext.Platforms.Include(p => p.Cost).ToListAsync();
	}

	public Task<Platform?> GetPlatformByIdAsync(int id)
	{
		return dbContext.Platforms.Include(p => p.Cost).FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task AddPlatformAsync(Platform platform)
	{
        ArgumentNullException.ThrowIfNull(platform);

        await dbContext.Platforms.AddAsync(platform);
	}
}
