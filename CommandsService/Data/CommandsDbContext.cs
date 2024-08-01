using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Data;

public class CommandsDbContext : DbContext
{
	public DbSet<Platform> Platforms;
	public DbSet<Command> Commands;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Platform>().HasMany(p => p.Commands).WithOne(c => c.Platform);
		
		modelBuilder.Entity<Command>().HasOne(c => c.Platform).WithMany(p => p.Commands);
	}
}
