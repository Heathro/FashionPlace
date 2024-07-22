using System.Net;
using System.Net.Http.Json;
using Contracts;

namespace CatalogService.IntegrationTests;

public class EventBusTests : BaseCatalogTest
{
    public EventBusTests(CustomWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task CreateProduct_WithValidObject_PublishProductAdded()
    {
        var createProductDto = ModelHelper.GetCreateProductDto();
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("admin"));

        var response = await _client.PostAsJsonAsync(PRODUCT_CONTROLLER_URL, createProductDto);

        response.EnsureSuccessStatusCode();
        Assert.True(await _testHarness.Published.Any<ProductAdded>());
    }
}
