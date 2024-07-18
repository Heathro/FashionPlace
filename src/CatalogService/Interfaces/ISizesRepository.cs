using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface ISizesRepository
{
    Task<List<string>> GetSizeNamesAsync();
    Task<Size> GetSizeAsync(string name);
    void AddSize(Size size);
}
