using Microsoft.EntityFrameworkCore;
using CatalogService.Interfaces;
using CatalogService.Entities;

namespace CatalogService.Data;

public class SizesRepository : ISizesRepository
{
    private readonly CatalogDbContext _context;

    public SizesRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetSizeNamesAsync()
    {
        return await _context.Sizes.Select(s => s.Name).ToListAsync();
    }

    public async Task<Size> GetSizeAsync(string name)
    {
        return await _context.Sizes.FirstOrDefaultAsync(s => s.Name == name);
    }

    public void AddSize(Size size)
    {
        _context.Sizes.Add(size);
    }
}
