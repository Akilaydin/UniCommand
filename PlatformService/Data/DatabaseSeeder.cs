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

		Console.WriteLine("Seeding cost types...");

		var freeCost = new CostType {
			Description = "Free",
			Amount = 0,
			CostStrategy = CostStrategy.Free
		};

		var oneTimeCost = new CostType {
			Description = "One-time cost",
			Amount = 150,
			CostStrategy = CostStrategy.OneTime
		};

		var subscriptionCost = new CostType {
			Description = "Subscription",
			Amount = 15,
			CostStrategy = CostStrategy.Subscription
		};

		var costTypes = new[] { freeCost, oneTimeCost, subscriptionCost };

		context.CostTypes.AddRange(costTypes);

		Console.WriteLine("Seeding platforms...");

		var platforms = new[] {
			new Platform {
				Name = "Dot Net",
				Publisher = "Microsoft",
				Cost = freeCost
			},
			new Platform {
				Name = "SQL Server",
				Publisher = "Microsoft",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "Paid Linux",
				Publisher = "Microsoft",
				Cost = oneTimeCost
			},
			new Platform {
				Name = "Ubuntu",
				Publisher = "Canonical",
				Cost = freeCost
			},
			new Platform {
				Name = "Red Hat Enterprise Linux",
				Publisher = "Red Hat",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "Windows 10",
				Publisher = "Microsoft",
				Cost = oneTimeCost
			},
			new Platform {
				Name = "macOS",
				Publisher = "Apple",
				Cost = freeCost
			},
			new Platform {
				Name = "Android",
				Publisher = "Google",
				Cost = freeCost
			},
			new Platform {
				Name = "iOS",
				Publisher = "Apple",
				Cost = freeCost
			},
			new Platform {
				Name = "Oracle Database",
				Publisher = "Oracle",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "MySQL",
				Publisher = "Oracle",
				Cost = freeCost
			},
			new Platform {
				Name = "PostgreSQL",
				Publisher = "PostgreSQL Global Development Group",
				Cost = freeCost
			},
			new Platform {
				Name = "MongoDB",
				Publisher = "MongoDB, Inc.",
				Cost = freeCost
			},
			new Platform {
				Name = "Apache Cassandra",
				Publisher = "Apache Software Foundation",
				Cost = freeCost
			},
			new Platform {
				Name = "Adobe Creative Cloud",
				Publisher = "Adobe",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "VMware ESXi",
				Publisher = "VMware, Inc.",
				Cost = oneTimeCost
			},
			new Platform {
				Name = "Docker",
				Publisher = "Docker, Inc.",
				Cost = freeCost
			},
			new Platform {
				Name = "Kubernetes",
				Publisher = "Cloud Native Computing Foundation",
				Cost = freeCost
			},
			new Platform {
				Name = "Salesforce",
				Publisher = "Salesforce",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "SAP HANA",
				Publisher = "SAP",
				Cost = subscriptionCost
			},
			new Platform {
				Name = "Git",
				Publisher = "Software Freedom Conservancy",
				Cost = freeCost
			},
			new Platform {
				Name = "Unity",
				Publisher = "Unity Technologies",
				Cost = subscriptionCost
			}
		};

		await context.Platforms.AddRangeAsync(platforms);

		await context.SaveChangesAsync();
	}
}
