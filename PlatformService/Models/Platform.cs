using System.ComponentModel.DataAnnotations;

using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Models;

public class Platform
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Publisher { get; set; }
	public CostType Cost { get; set; }
}
