using OriApps.UniCommand.PlatformService.Data.DTO;

namespace OriApps.UniCommand.PlatformService.Services;

public interface IMessageBusClient : IDisposable
{
	void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO);
}