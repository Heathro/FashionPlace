using MongoDB.Entities;
using MassTransit;
using AutoMapper;
using Contracts;
using SearchService.Entities;

namespace SearchService.Messages;

public class ProductAddedConsumer : IConsumer<ProductAdded>
{
    private readonly IMapper _mapper;

    public ProductAddedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ProductAdded> context)
    {
        Console.WriteLine("\n\n\n======>>>>>> Received product: " + context.Message.Id + "\n\n\n");

        var product = _mapper.Map<Product>(context.Message);

        var searchStringList = new List<string>
        {
            product.Description,
            product.Brand,
            product.Model
        };

        foreach (var productCategory in product.ProductCategories)
        {
            searchStringList.AddRange(productCategory.Categories);
        }

        searchStringList.AddRange(product.Variants.Select(v => v.Color));
        searchStringList.AddRange(product.Variants.Select(v => v.Size));
        searchStringList.AddRange(product.Specifications.Select(s => s.Value));

        product.SearchString = string.Join(" ", searchStringList);

        await product.SaveAsync();
    }
}
