using CatalogService.DTOs;
using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface IProductsRepository
{
    Task<List<ProductDto>> GetProductDtosAsync();
    Task<ProductDto> GetProductDtoAsync(Guid id);
    Task<Product> GetProductAsync(Guid id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
}
