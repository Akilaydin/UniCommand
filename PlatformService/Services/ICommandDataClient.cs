using OriApps.UniCommand.PlatformService.Data.DTO;

namespace OriApps.UniCommand.PlatformService.Services;

public interface ICommandDataClient
{
	Task SendPlatformToCommand(PlatformReadDTO platformReadDTO);
}