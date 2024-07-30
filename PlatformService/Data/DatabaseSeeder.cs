#region
using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Models;
using OriApps.UniCommand.PlatformService.Models.Cost;
#endregion

namespace OriApps.UniCommand.PlatformService.Data;

public static class DatabaseSeeder
{
	public static async Task SeedAsync(IApplicationBuilder builder)
	{
		using var scope = builder.ApplicationServices.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<PlatformServiceDbContext>();

		if (await context.Platforms.AnyAsync())
		{
			Console.WriteLine("Database already seeded.");

			return;
		}
		
		Console.WriteLine("Seeding platforms...");

		var platforms = new[] {
			new Platform {
				Name = "Dot Net",
				Publisher = "Microsoft",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "SQL Server",
				Publisher = "Microsoft",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "Paid Linux",
				Publisher = "Microsoft",
				Cost = new OneTimeCostType()
			},
			new Platform {
				Name = "Ubuntu",
				Publisher = "Canonical",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Red Hat Enterprise Linux",
				Publisher = "Red Hat",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "Windows 10",
				Publisher = "Microsoft",
				Cost = new OneTimeCostType()
			},
			new Platform {
				Name = "macOS",
				Publisher = "Apple",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Android",
				Publisher = "Google",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "iOS",
				Publisher = "Apple",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Oracle Database",
				Publisher = "Oracle",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "MySQL",
				Publisher = "Oracle",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "PostgreSQL",
				Publisher = "PostgreSQL Global Development Group",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "MongoDB",
				Publisher = "MongoDB, Inc.",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Apache Cassandra",
				Publisher = "Apache Software Foundation",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Adobe Creative Cloud",
				Publisher = "Adobe",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "VMware ESXi",
				Publisher = "VMware, Inc.",
				Cost = new OneTimeCostType()
			},
			new Platform {
				Name = "Docker",
				Publisher = "Docker, Inc.",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Kubernetes",
				Publisher = "Cloud Native Computing Foundation",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Salesforce",
				Publisher = "Salesforce",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "SAP HANA",
				Publisher = "SAP",
				Cost = new SubscriptionCostType()
			},
			new Platform {
				Name = "Git",
				Publisher = "Software Freedom Conservancy",
				Cost = new FreeCostType()
			},
			new Platform {
				Name = "Unity",
				Publisher = "Unity Technologies",
				Cost = new SubscriptionCostType()
			}
		};

		await context.Platforms.AddRangeAsync(platforms);
		
		await context.SaveChangesAsync();
	}
}
