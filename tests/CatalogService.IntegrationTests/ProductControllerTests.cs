using System.Net;
using System.Net.Http.Json;
using CatalogService.DTOs;

namespace CatalogService.IntegrationTests;

public class ProductControllerTests : BaseCatalogTest
{
    public ProductControllerTests(CustomWebAppFactory factory) : base(factory) { }

    [Fact]
    public async Task GetProducts_With3ProductsInDatabase_Return3Products()
    {
        var response = await _client.GetFromJsonAsync<List<ProductDto>>(PRODUCT_CONTROLLER_URL);

        Assert.NotNull(response);
        Assert.Equal(3, response.Count);
        Assert.IsType<List<ProductDto>>(response);
    }

    [Fact]
    public async Task GetProduct_WithValidId_ReturnProduct()
    {
        var response = await _client.GetFromJsonAsync<ProductDto>(PRODUCT_CONTROLLER_URL + VALID_PRODUCT_ID);

        Assert.NotNull(response);
        Assert.Equal(Guid.Parse(VALID_PRODUCT_ID), response.Id);
        Assert.IsType<ProductDto>(response);
    }

    [Fact]
    public async Task GetProduct_WithInvalidId_ReturnNotFound()
    {
        var response = await _client.GetAsync(PRODUCT_CONTROLLER_URL + Guid.NewGuid());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CetProduct_WithInvalidGuid_ReturnBadRequest()
    {
        var response = await _client.GetAsync(PRODUCT_CONTROLLER_URL + "not-a-guid");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithNoAuthorization_ReturnUnauthorized()
    {
        var product = new CreateProductDto();

        var response = await _client.PostAsJsonAsync(PRODUCT_CONTROLLER_URL, product);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WithAuthorization_ReturnCreatedAt()
    {
        var product = ModelHelper.GetCreateProductDto();
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("admin"));

        var response = await _client.PostAsJsonAsync(PRODUCT_CONTROLLER_URL, product);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<ProductDto>(await response.Content.ReadFromJsonAsync<ProductDto>());
    }

    [Fact]
    public async Task CreateProduct_WithInvalidModel_ReturnBadRequest()
    {
        var product = ModelHelper.GetCreateProductDto();
        product.Brand = null;
        _client.SetFakeJwtBearerToken(AuthHelper.GetBearerForUser("admin"));

        var response = await _client.PostAsJsonAsync(PRODUCT_CONTROLLER_URL, product);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}