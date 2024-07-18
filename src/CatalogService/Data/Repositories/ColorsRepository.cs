using Microsoft.EntityFrameworkCore;
using CatalogService.Interfaces;
using CatalogService.Entities;

namespace CatalogService.Data;

public class ColorsRepository : IColorsRepository
{
    private readonly CatalogDbContext _context;

    public ColorsRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetColorNamesAsync()
    {
        return await _context.Colors.Select(c => c.Name).ToListAsync();
    }

    public async Task<Color> GetColorAsync(string name)
    {
        return await _context.Colors.FirstOrDefaultAsync(c => c.Name == name);
    }

    public void AddColor(Color color)
    {
        _context.Colors.Add(color);
    }
}
