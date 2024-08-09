using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface IBrandsRepository
{
    Task<List<string>> GetBrandNamesAsync();
    Task<Brand> GetBrandAsync(string name);
    void RemoveBrand(Brand brand);
}
