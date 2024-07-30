using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Models;
using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Data;

public class PlatformServiceDbContext(DbContextOptions<PlatformServiceDbContext> options) : DbContext(options)
{
	public DbSet<Platform> Platforms { get; init; }
	public DbSet<CostType> CostTypes { get; init; }
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Platform>().HasOne(p => p.Cost).WithMany().HasForeignKey("costTypeId");
	}
}
