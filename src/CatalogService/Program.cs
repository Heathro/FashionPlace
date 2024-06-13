using Microsoft.EntityFrameworkCore;
using MassTransit;
using CatalogService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<CatalogDbContext>(options =>
    {
        options.QueryDelay = TimeSpan.FromSeconds(10);
        options.UsePostgres();
        options.UseBusOutbox();
    });
    x.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine("\n\n\n======>>>>>> Failed to seed database. Error: " + e.Message + ".\n\n\n");
}

app.Run();
