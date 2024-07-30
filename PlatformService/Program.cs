using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PlatformServiceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<PlatformServiceDbContext>(options => options.UseInMemoryDatabase("InMemoryPlatformServiceDb"));

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

var app = builder.Build();

await DatabaseSeeder.SeedAsync(app);

app.Run();