using System.ComponentModel.DataAnnotations;

using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Data.DTO;

public class PlatformCreateDTO
{
	[Required]
	public string Name { get; set; }
	
	[Required]
	public string Publisher { get; set; }
	
	[Required]
	public CostType Cost { get; set; }
}
