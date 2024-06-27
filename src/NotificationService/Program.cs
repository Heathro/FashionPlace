using MassTransit;
using NotificationService.Hubs;
using NotificationService.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMassTransit(x =>
    {
        x.AddConsumersFromNamespaceContaining<ProductAddedConsumer>();
        x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("notification", false));
        x.UsingRabbitMq((context, config) =>
        {
            config.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
            {
                host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
            });
            config.ConfigureEndpoints(context);
        });
    });

builder.Services
    .AddSignalR();

var app = builder.Build();

app.MapHub<NotificationHub>("/notification");

app.Run();
