using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.PlatformService.Data;
using OriApps.UniCommand.PlatformService.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PlatformServiceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<PlatformServiceDbContext>(options => options.UseInMemoryDatabase("InMemoryPlatformServiceDb"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

await DatabaseSeeder.SeedAsync(app);

app.Run();