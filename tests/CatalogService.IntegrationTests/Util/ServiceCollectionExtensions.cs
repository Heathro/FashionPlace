using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.Data;

namespace CatalogService.IntegrationTests;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault
        (
            d => d.ServiceType == typeof(DbContextOptions<CatalogDbContext>)
        );

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static void EnsureCreated<T>(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        
        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<CatalogDbContext>();

        context.Database.Migrate();
        DbHelper.InitDbForTests(context);
    }
}
