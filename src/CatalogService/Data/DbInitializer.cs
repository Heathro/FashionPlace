using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        scope.ServiceProvider.GetService<CatalogDbContext>().Database.Migrate();
    }
}
