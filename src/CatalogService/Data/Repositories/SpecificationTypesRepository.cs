using Microsoft.EntityFrameworkCore;
using CatalogService.Interfaces;
using CatalogService.Entities;

namespace CatalogService.Data;

public class SpecificationTypesRepository : ISpecificationTypesRepository
{
    private readonly CatalogDbContext _context;

    public SpecificationTypesRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetSpecificationTypeNamesAsync()
    {
        return await _context.SpecificationTypes.Select(s => s.Type).ToListAsync();
    }

    public async Task<SpecificationType> GetSpecificationTypeAsync(string type)
    {
        return await _context.SpecificationTypes
            .Include(s => s.Specifications)
            .FirstOrDefaultAsync(s => s.Type == type);
    }
}
