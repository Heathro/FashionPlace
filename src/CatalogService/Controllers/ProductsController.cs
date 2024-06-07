using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CatalogService.Data;
using CatalogService.DTOs;

namespace CatalogService.Controllers;

[ApiController]
[Route("catalog/products")]
public class ProductsController : ControllerBase
{
    private readonly CatalogDbContext _context;
    private readonly IMapper _mapper;

    public ProductsController(CatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
    {
        var products = await _context.Products
            .Include(p => p.Model)
            .ThenInclude(m => m.Brand)
            .Include(p => p.Category)
            .Include(p => p.Specifications)
            .ThenInclude(s => s.SpecificationType)
            .ToListAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Model)
            .ThenInclude(m => m.Brand)
            .Include(p => p.Category)
            .Include(p => p.Specifications)
            .ThenInclude(s => s.SpecificationType)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        return _mapper.Map<ProductDto>(product);
    }
}
