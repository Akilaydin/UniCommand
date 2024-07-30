using System.ComponentModel.DataAnnotations;

namespace OriApps.UniCommand.PlatformService.Models.Cost;

public abstract class CostType
{
	[Key]
	public int Id { get; set; }
	public abstract decimal? GetCostAmount();
	public abstract string GetDescription();
}