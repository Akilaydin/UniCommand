namespace OriApps.UniCommand.PlatformService.Models.Cost;

public class SubscriptionCostType : CostType
{
	public decimal Amount { get; set; }
	public string Period { get; set; } = "Monthly";

	public override decimal? GetCostAmount() => Amount;
	public override string GetDescription() => $"Subscription ({Period}): {Amount}";
}