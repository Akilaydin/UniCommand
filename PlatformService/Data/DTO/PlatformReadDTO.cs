using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Data.DTO;

public class PlatformReadDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Publisher { get; set; }
	public CostType Cost { get; set; }
}
