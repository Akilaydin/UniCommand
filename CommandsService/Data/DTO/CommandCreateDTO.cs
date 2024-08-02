using System.ComponentModel.DataAnnotations;

namespace OriApps.UniCommand.CommandsService.Data.DTO;

public class CommandCreateDTO
{
	[Required]
	public string HowTo { get; set; }
	[Required]
	public string CommandLine { get; set; }
}
