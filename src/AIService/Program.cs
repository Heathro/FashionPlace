using Microsoft.EntityFrameworkCore;
using AIService.Data;
using AIService.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AIDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
    });
    
builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddSignalR();

var app = builder.Build();

app.MapHub<AIHub>("/ai");

await DbInitializer.InitDbAsync(app);

app.Run();
