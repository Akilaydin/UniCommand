namespace OriApps.UniCommand.CommandsService.EventProcessing;

public interface IEventProcessor
{
	Task ProcessEventAsync(string message);
}