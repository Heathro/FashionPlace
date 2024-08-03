using Microsoft.EntityFrameworkCore;
using MassTransit;
using AIService.Data;
using AIService.Hubs;
using AIService.Services;
using AIService.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddScoped<ModelService>();

builder.Services
    .AddHttpClient<ModelHttpClient>();

builder.Services
    .AddDbContext<AIDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"));
    });

builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddMassTransit(x =>
    {
        x.AddConsumersFromNamespaceContaining<ProductAddedConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ai", false));
        x.UsingRabbitMq((context, config) =>
        {
            config.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            config.ReceiveEndpoint("ai-product-added", endpoint =>
            {
                endpoint.UseMessageRetry(retry => retry.Interval(5, 5));
                endpoint.ConfigureConsumer<ProductAddedConsumer>(context);
            });
            config.ConfigureEndpoints(context);
        });
    });

builder.Services
    .AddSignalR();

var app = builder.Build();

app.MapHub<AIHub>("/ai");

await DbInitializer.InitDbAsync(app);

app.Run();
