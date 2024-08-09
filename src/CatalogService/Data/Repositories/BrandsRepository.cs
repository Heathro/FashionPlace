using Microsoft.EntityFrameworkCore;
using CatalogService.Interfaces;
using CatalogService.Entities;

namespace CatalogService.Data;

public class BrandsRepository : IBrandsRepository
{
    private readonly CatalogDbContext _context;

    public BrandsRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetBrandNamesAsync()
    {
        return await _context.Brands.Select(b => b.Name).ToListAsync();
    }

    public async Task<Brand> GetBrandAsync(string name)
    {
        return await _context.Brands
            .Include(b => b.Models)
            .FirstOrDefaultAsync(b => b.Name == name);
    }

    public void RemoveBrand(Brand brand)
    {
        _context.Brands.Remove(brand);
    }
}
