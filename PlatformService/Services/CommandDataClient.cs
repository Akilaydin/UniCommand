#region
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using OriApps.UniCommand.PlatformService.Data.DTO;
#endregion

namespace OriApps.UniCommand.PlatformService.Services;

public class CommandDataClient(HttpClient httpClient, IConfiguration configuration) : ICommandDataClient
{
	public async Task SendPlatformToCommand(PlatformReadDTO platformReadDTO)
	{
		var content = new StringContent(JsonSerializer.Serialize(platformReadDTO), Encoding.UTF8, "application/json");
		
		var baseUrl = configuration["CommandDataBaseUrl"];
		
		ArgumentNullException.ThrowIfNull(baseUrl);

		string? targetUrl = baseUrl + "/api/commands/platforms/";

		var response = await httpClient.PostAsync(targetUrl, content);

		Console.WriteLine(response.IsSuccessStatusCode 
			? $"Platform sent successfully {await response.Content.ReadAsStringAsync()}"
			: $"Error while sending platform to command: {response.StatusCode}");
	}
}
