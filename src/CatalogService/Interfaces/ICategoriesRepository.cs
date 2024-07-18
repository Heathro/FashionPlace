using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface ICategoriesRepository
{
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(Guid? id);
}
