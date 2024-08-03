using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.CommandsService.BackgroundServices;
using OriApps.UniCommand.CommandsService.Data;
using OriApps.UniCommand.CommandsService.EventProcessing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommandsDbContext>(op => op.UseInMemoryDatabase("CommandsDb"));

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
