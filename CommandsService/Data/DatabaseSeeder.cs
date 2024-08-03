using OriApps.UniCommand.CommandsService.Models;
using OriApps.UniCommand.CommandsService.Services;

namespace OriApps.UniCommand.CommandsService.Data;

public class DatabaseSeeder
{
	public static async Task SeedAsync(IApplicationBuilder builder)
	{
		using var scope = builder.ApplicationServices.CreateScope();
		var grpcClient = scope.ServiceProvider.GetRequiredService<IPlatformDataClient>();
		var platformData = grpcClient.GetAllPlatforms();
		
		await SeedDataAsync(scope.ServiceProvider.GetRequiredService<ICommandRepository>(), platformData);
	}

	private static async Task SeedDataAsync(ICommandRepository commandRepository, IEnumerable<Platform> platforms)
	{
		
		foreach (var platform in platforms)
		{
			if (await commandRepository.PlatformExistsAsync(platform.Id))
			{
				continue;
			}
			
			await commandRepository.CreatePlatformAsync(platform);

			await commandRepository.SaveChangesAsync();
		}
	}
}
