using AutoMapper;

using Grpc.Net.Client;

using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Services;

public class PlatformDataClient(IConfiguration configuration, IMapper mapper) : IPlatformDataClient
{
	public IEnumerable<Platform> GetAllPlatforms()
	{
		string address = configuration["GrpcPlatform"];
		
		ArgumentNullException.ThrowIfNull(address);
		
		Console.WriteLine($"Getting all platforms {address}");
		
		var channel = GrpcChannel.ForAddress(address);
		var client = new GrpcPlatform.GrpcPlatformClient(channel);
		var request = new GetAllRequest();

		try
		{
			var response = client.GetAllPlatforms(request);
			
			return mapper.Map<IEnumerable<Platform>>(response.Platform);
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error while getting all platforms {e.Message}");
			throw;
		}
	}
}
