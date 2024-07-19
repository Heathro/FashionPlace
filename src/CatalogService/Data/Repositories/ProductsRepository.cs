using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CatalogService.DTOs;
using CatalogService.Entities;
using CatalogService.Interfaces;

namespace CatalogService.Data;

public class ProductsRepository : IProductsRepository
{
    private readonly CatalogDbContext _context;
    private readonly IMapper _mapper;

    public ProductsRepository(CatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Model)
                .ThenInclude(m => m.Brand)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                    .ThenInclude(c => c.ParentCategory)
                        .ThenInclude(c => c.ParentCategory)
                            .ThenInclude(c => c.ParentCategory)
                                .ThenInclude(c => c.ParentCategory)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Color)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Size)
            .Include(p => p.Specifications)
                .ThenInclude(s => s.SpecificationType)
            .ToListAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductAsync(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Model)
                .ThenInclude(m => m.Brand)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                    .ThenInclude(c => c.ParentCategory)
                        .ThenInclude(c => c.ParentCategory)
                            .ThenInclude(c => c.ParentCategory)
                                .ThenInclude(c => c.ParentCategory)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Color)
            .Include(p => p.Variants)
                .ThenInclude(v => v.Size)
            .Include(p => p.Specifications)
                .ThenInclude(s => s.SpecificationType)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductDto>(product);
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
    }
}
