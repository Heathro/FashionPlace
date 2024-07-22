using CatalogService.Data;

namespace CatalogService.IntegrationTests;

public static class DbHelper
{
    public static void InitDbForTests(CatalogDbContext context)
    {
        context.Products.AddRange(ModelHelper.GetProductsForTests());
        context.SaveChanges();
    }

    public static void ReinitDbForTests(CatalogDbContext context)
    {
        context.Products.RemoveRange(context.Products);
        context.SaveChanges();
        InitDbForTests(context);
    }
}
