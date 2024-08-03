using MassTransit;
using SearchService.Data;
using SearchService.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddMassTransit(x =>
    {
        x.AddConsumersFromNamespaceContaining<ProductAddedConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
        x.UsingRabbitMq((context, config) =>
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

await DbInitializer.InitDbAsync(app);

app.Run();
