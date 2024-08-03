using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Services;

public interface IPlatformDataClient
{
	IEnumerable<Platform> GetAllPlatforms();
}