using System.ComponentModel.DataAnnotations;

namespace OriApps.UniCommand.PlatformService.Data.DTO;

public class PlatformCreateDTO
{
	[Required]
	public string Name { get; set; }
	
	[Required]
	public string Publisher { get; set; }
	
	[Required]
	public int CostTypeID { get; set; }
}
