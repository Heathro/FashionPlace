using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDbAsync(WebApplication app)
    {
        await DB.InitAsync
        (
            "Search",
            MongoClientSettings.FromConnectionString
            (
                app.Configuration.GetConnectionString("MongoDb")
            )
        );

        await DB.Index<Product>()
            .Key(p => p.Description, KeyType.Text)
            .Key(p => p.Brand, KeyType.Text)
            .Key(p => p.Model, KeyType.Text)
            .Key(p => p.SearchString, KeyType.Text)
            .CreateAsync();
    }
}
