using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;
using MassTransit;
using Contracts;

namespace NotificationService.Messages;

public class ProductAddedConsumer : IConsumer<ProductAdded>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public ProductAddedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<ProductAdded> context)
    {
        Console.WriteLine("\n\n\n======>>>>>> Product Added: " + context.Message.Id + "\n\n\n");

        await _hubContext.Clients.All.SendAsync("ProductAdded", context.Message);
    }
}
