using Microsoft.EntityFrameworkCore;
using CatalogService.Entities;
using CatalogService.Interfaces;

namespace CatalogService.Data;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly CatalogDbContext _context;

    public CategoriesRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(Guid? id)
    {
        return await _context.Categories
                    .Include(c => c.ParentCategory)
                        .ThenInclude(c => c.ParentCategory)
                            .ThenInclude(c => c.ParentCategory)
                                .ThenInclude(c => c.ParentCategory)
                    .FirstOrDefaultAsync(c => c.Id == id);
    }
}
