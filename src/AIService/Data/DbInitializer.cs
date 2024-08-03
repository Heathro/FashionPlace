using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;
using AIService.Entities;

namespace AIService.Data;

public class DbInitializer
{
    public static async Task InitDbAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<AIDbContext>();
        
        await context.Database.MigrateAsync();

        context.MessageThreads.RemoveRange(context.MessageThreads);
		await context.SaveChangesAsync();

        await DB.InitAsync
        (
            "AI",
            MongoClientSettings.FromConnectionString
            (
                app.Configuration.GetConnectionString("MongoDb")
            )
        );

        await DB.Index<Product>()
            .Key(p => p.Description, KeyType.Text)
            .Key(p => p.Brand, KeyType.Text)
            .Key(p => p.Model, KeyType.Text)
            .Key(p => p.Categories, KeyType.Text)
            .Key(p => p.Colors, KeyType.Text)
            .Key(p => p.Sizes, KeyType.Text)
            .Key(p => p.Specifications, KeyType.Text)
            .CreateAsync();
    }
}
