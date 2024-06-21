using MassTransit;
using SearchService.Data;
using SearchService.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumersFromNamespaceContaining<ProductAddedConsumer>();
    configure.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
    configure.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        config.ReceiveEndpoint("search-product-added", endpoint =>
        {
            endpoint.UseMessageRetry(retry => retry.Interval(5, 5));
            endpoint.ConfigureConsumer<ProductAddedConsumer>(context);
        });
        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

try
{
    await DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine("\n\n\n======>>>>>> Failed to seed database. Error: " + e.Message + ".\n\n\n");
}

app.Run();
