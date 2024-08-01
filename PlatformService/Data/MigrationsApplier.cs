using Microsoft.EntityFrameworkCore;

namespace OriApps.UniCommand.PlatformService.Data;

public static class MigrationsApplier
{
	public static async Task ApplyMigrationsAsync(IApplicationBuilder builder)
	{
		using var scope = builder.ApplicationServices.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<PlatformServiceDbContext>();

		try
		{
			await context.Database.MigrateAsync();
		} 
		catch (Exception e)
		{
			Console.WriteLine($"Error migrating database: {e}");
			throw;
		}
	}
}
