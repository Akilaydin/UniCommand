using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.CommandsService.BackgroundServices;
using OriApps.UniCommand.CommandsService.Data;
using OriApps.UniCommand.CommandsService.EventProcessing;
using OriApps.UniCommand.CommandsService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommandsDbContext>(op => op.UseInMemoryDatabase("CommandsDb"));

var app = builder.Build();

app.UseRouting();
app.MapControllers();

await DatabaseSeeder.SeedAsync(app);

app.Run();
