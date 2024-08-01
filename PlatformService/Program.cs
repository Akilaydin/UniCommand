using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Data;
using OriApps.UniCommand.PlatformService.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Running in Development Mode = " + builder.Environment.IsDevelopment());

if (builder.Environment.IsDevelopment())
{
	builder.Services.AddDbContext<PlatformServiceDbContext>(options => options.UseInMemoryDatabase("InMemoryPlatformServiceDb"));
}
else
{
	builder.Services.AddDbContext<PlatformServiceDbContext>(options =>
	{
		var connectionStringBase = builder.Configuration.GetConnectionString("DefaultConnection");
		
		Console.WriteLine($"ConnectionString = {connectionStringBase}");
		
		ArgumentNullException.ThrowIfNull(connectionStringBase);
		
		var connectionStringWithPassword = connectionStringBase.Replace("{POSTGRES_PASSWORD}", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));

		Console.WriteLine($"Db connection string = {connectionStringWithPassword}");
			
		options.UseNpgsql(connectionStringWithPassword);
	});
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

if (app.Environment.IsProduction())
{
	await MigrationsApplier.ApplyMigrationsAsync(app);
}

await DatabaseSeeder.SeedAsync(app);

app.Run();