using Microsoft.EntityFrameworkCore;

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
    }
}
