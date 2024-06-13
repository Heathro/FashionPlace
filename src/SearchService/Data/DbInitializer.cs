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

        await DB.Index<Product>()
            .Key(p => p.Brand, KeyType.Text)
            .Key(p => p.Model, KeyType.Text)
            .Key(p => p.Category, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Product>();
        if (count > 0)
        {
            Console.WriteLine("======>>>>>> No seeding needed. Database already contains data.");
        }
        else
        {
            Console.WriteLine("======>>>>>> Seeding data.");

            var data = await File.ReadAllTextAsync("Data/products.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var products = JsonSerializer.Deserialize<List<Product>>(data, options);
            await DB.SaveAsync(products);
        }
    }
}
