using Microsoft.EntityFrameworkCore;

using OriApps.UniCommand.CommandsService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommandsDbContext>(op => op.UseInMemoryDatabase("CommandsDb"));

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
