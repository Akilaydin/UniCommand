using System.ComponentModel.DataAnnotations;

namespace OriApps.UniCommand.PlatformService.Models.Cost;

public class CostType
{
	[Key]
	public int Id { get; set; }
	[MaxLength(3000)]
	public string Description { get; init; } = string.Empty;
	public decimal? Amount { get; init; }
	public CostStrategy CostStrategy { get; init; }
}