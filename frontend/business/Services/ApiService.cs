using System.Net.Http.Headers;
using System.Net.Http.Json;
using business.DTOs;

namespace business.Services;

public class ApiService
{
    public async Task<List<ProductDto>> GetProducts()
    {
        var httpClient = new HttpClient();

		var products = await httpClient.GetFromJsonAsync<List<ProductDto>>
		(
			"http://localhost:6001/catalog/products"
		);

        return products;
    }
	
	public async Task<ProductDto> GetProduct(Guid id)
    {
        var httpClient = new HttpClient();

		var products = await httpClient.GetFromJsonAsync<ProductDto>
		(
			"http://localhost:6001/catalog/products/" + id
		);

        return products;
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto createProductDto)
	{
        var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
		(
			"Bearer",
			await SecureStorage.GetAsync("access_token")
		);

		var response = await httpClient.PostAsJsonAsync
		(
			"http://localhost:6001/catalog/products",
			createProductDto
		);

		return response.Content.ReadFromJsonAsync<ProductDto>().Result;
	}

	public async Task<List<CategoryDto>> GetCategories()
	{
		var httpClient = new HttpClient();

		var categories = await httpClient.GetFromJsonAsync<List<CategoryDto>>
		(
			"http://localhost:6001/catalog/categories"
		);

		return categories;
	}
}
