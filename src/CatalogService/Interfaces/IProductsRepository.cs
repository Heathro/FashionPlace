using CatalogService.DTOs;
using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface IProductsRepository
{
    Task<List<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductAsync(Guid id);
    public void AddProduct(Product product);
    Task<bool> SaveChangesAsync();
}
