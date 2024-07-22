using Microsoft.Extensions.DependencyInjection;
using MassTransit.Testing;
using CatalogService.Data;

namespace CatalogService.IntegrationTests;

[Collection("Shared collection")]
public class BaseCatalogTest : IAsyncLifetime
{
    protected readonly CustomWebAppFactory _factory;
    protected readonly HttpClient _client;
    protected readonly ITestHarness _testHarness;
    protected const string PRODUCT_CONTROLLER_URL = "api/catalog/products/";
    protected const string VALID_PRODUCT_ID = "1a5e2d70-2b74-4d49-8f37-8bfa601d5d42";

    public BaseCatalogTest(CustomWebAppFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _testHarness = _factory.Services.GetTestHarness();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        DbHelper.ReinitDbForTests(context);
        return Task.CompletedTask;
    }
}
