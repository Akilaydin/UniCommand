namespace OriApps.UniCommand.PlatformService.Models.Cost;

public class OneTimeCostType : CostType
{
	public decimal Amount { get; set; }
	public override decimal? GetCostAmount() => Amount;
	public override string GetDescription() => $"One-time cost: {Amount}";
}