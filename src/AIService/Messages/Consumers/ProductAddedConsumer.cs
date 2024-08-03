using MongoDB.Entities;
using MassTransit;
using AutoMapper;
using Contracts;
using AIService.Entities;

namespace AIService.Messages;

public class ProductAddedConsumer : IConsumer<ProductAdded>
{
    private readonly IMapper _mapper;

    public ProductAddedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ProductAdded> context)
    {
        Console.WriteLine("\n\n\n======>>>>>> Product Added: " + context.Message.Id + "\n\n\n");

        var product = _mapper.Map<Product>(context.Message);

        await product.SaveAsync();
    }
}
