namespace OriApps.UniCommand.PlatformService.Models.Cost;

public class FreeCostType : CostType
{
	public override decimal? GetCostAmount() => 0;
	public override string GetDescription() => "Free";
}