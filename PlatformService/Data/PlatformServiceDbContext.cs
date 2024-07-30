using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Models;
using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Data;

public class PlatformServiceDbContext : DbContext
{
	public DbSet<Platform> Platforms { get; set; }
	
	public PlatformServiceDbContext(DbContextOptions<PlatformServiceDbContext> options) : base(options)
	{
		
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CostType>()
			.ToTable("CostTypes")
			.HasDiscriminator<string>("CostType")
			.HasValue<FreeCostType>("Free")
			.HasValue<OneTimeCostType>("OneTime")
			.HasValue<SubscriptionCostType>("Subscription");
	}
}
