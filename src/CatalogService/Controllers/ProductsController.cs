using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using AutoMapper;
using Contracts;
using CatalogService.Data;
using CatalogService.DTOs;
using CatalogService.Entities;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly CatalogDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductsController(CatalogDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
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
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Model)
            .ThenInclude(m => m.Brand)
            .Include(p => p.Category)
            .Include(p => p.Specifications)
            .ThenInclude(s => s.SpecificationType)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductDto>(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        var product = _mapper.Map<Product>(createProductDto);

        _context.Products.Add(product);

        var newProduct = _mapper.Map<ProductDto>(product);

        var productAdded = _mapper.Map<ProductAdded>(newProduct);
        await _publishEndpoint.Publish(productAdded);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Failed to create product");

        return CreatedAtAction(nameof(GetProduct), new { product.Id }, newProduct);
    }
}
