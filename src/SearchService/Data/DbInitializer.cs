using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync
        (
            "Search",
            MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDb"))
        );

        await DB.Index<Product>().Key(p => p.SearchString, KeyType.Text).CreateAsync();

        var count = await DB.CountAsync<Product>();
        if (count > 0)
        {
            Console.WriteLine("\n\n\n======>>>>>> No seeding needed. Database already contains data.\n\n\n");
        }
        else
        {
            Console.WriteLine("\n\n\n======>>>>>> Seeding data.\n\n\n");

            var data = await File.ReadAllTextAsync("Data/products.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var products = JsonSerializer.Deserialize<List<Product>>(data, options);

            foreach (var product in products)
            {
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
            }

            await DB.SaveAsync(products);
        }
    }
}
