using System.Text.Json;

using AutoMapper;

using OriApps.UniCommand.CommandsService.Data;
using OriApps.UniCommand.CommandsService.Data.DTO;
using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.EventProcessing;

public class EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper) : IEventProcessor
{
	public async Task ProcessEventAsync(string message)
	{
		var eventType = DetermineEventType(message);

		switch (eventType)
		{
			case
				EventType.PlatformPublished: await AddPlatform(message);
				break;
			default:
				return;
		}
	}

	private async Task AddPlatform(string platformPublishedMessage)
	{
		using var scope = serviceScopeFactory.CreateScope();
		var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
		
		var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDTO>(platformPublishedMessage);

		try
		{
			var platform = mapper.Map<Platform>(platformPublishedDto);

			if (await repository.PlatformExistsAsync(platform.Id))
			{
				Console.WriteLine("Platform already exists");
				
				return;
			}
			
			await repository.CreatePlatformAsync(mapper.Map<Platform>(platformPublishedDto));
			await repository.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error while creating platform {e}");
			throw;
		}
	}

	private EventType DetermineEventType(string message)
	{
		Console.WriteLine($"Determine Event Type ({message})");

		var eventType = JsonSerializer.Deserialize<GenericEventDTO>(message);
		
		ArgumentNullException.ThrowIfNull(eventType);
		
		Console.WriteLine($"Event is of type {eventType.Event}");

		return eventType.Event switch {
			"PlatformPublished" => EventType.PlatformPublished,
			_ => EventType.Unknown
		};
	}
}
