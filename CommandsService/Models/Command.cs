using System.ComponentModel.DataAnnotations;

namespace OriApps.UniCommand.CommandsService.Models;

public class Command
{
	[Key]
	[Required]
	public int Id { get; set; }
	[Required]
	public string HowTo { get; set; }
	[Required]
	public string CommandLine { get; set; }
	
	public Platform Platform { get; set; }
}
