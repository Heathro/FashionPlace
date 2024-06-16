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
[Route("api/catalog")]
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

    [HttpGet("products")]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Model)
                .ThenInclude(m => m.Brand)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
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

    [HttpGet("product/{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Model)
                .ThenInclude(m => m.Brand)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
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

    [HttpGet("categories")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        var categoryDtos = BuildCategoryHierarchy(null, categories);
        return Ok(categoryDtos);
    }

    private List<CategoryDto> BuildCategoryHierarchy(Guid? parentId, List<Category> categories)
    {
        return categories
            .Where(c => c.ParentCategoryId == parentId)
            .Select(c => new CategoryDto
            {
                Name = c.Name,
                SubCategories = BuildCategoryHierarchy(c.Id, categories)
            })
            .ToList();
    }

    // [HttpPost]
    // public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    // {
    //     Brand brand;
    //     if (createProductDto.Model.BrandId == Guid.Empty)
    //     {
    //         brand = new Brand { Name = createProductDto.Model.BrandName };
    //         _context.Brands.Add(brand);
    //         await _context.SaveChangesAsync();
    //     }
    //     else
    //     {
    //         brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == createProductDto.Model.BrandId);
    //     }

    //     // GetExistingOrCreateNewBrand(createProductDto.Model.BrandName);

    //     var product = new Product()
    //     {
    //         Description = createProductDto.Description,
    //         Model = new Model
    //         {
    //             Name = createProductDto.Model.Name,
    //             Brand = brand
    //         },
    //         Variants = new List<Variant>
    //             {
    //                 new Variant
    //                 {
    //                     Color = new Color { Name = "White" },
    //                     Size = new Size { Name = "S" },
    //                     Price = 10,
    //                     Discount = 0,
    //                     Quantity = 8,
    //                     ImageUrl = "imageUrl adidas strike white s"
    //                 },
    //                 new Variant
    //                 {
    //                     Color = new Color { Name = "Black" },
    //                     Size = new Size { Name = "XL" },
    //                     Price = 12,
    //                     Discount = 10,
    //                     Quantity = 4,
    //                     ImageUrl = "imageUrl adidas strike black xl"
    //                 }
    //             },
    //         Specifications = new List<Specification>
    //             {
    //                 new Specification
    //                 {
    //                     SpecificationType = new SpecificationType{ Type = "Fabric" },
    //                     Value = "Polyester"
    //                 },
    //                 new Specification
    //                 {
    //                     SpecificationType = new SpecificationType{ Type = "Length" },
    //                     Value = "Regular"
    //                 }
    //             }
    //     };

    //     _context.Products.Add(product);

    //     var newProduct = _mapper.Map<ProductDto>(product);

    //     //var productAdded = _mapper.Map<ProductAdded>(newProduct);
    //     //await _publishEndpoint.Publish(productAdded);

    //     var result = await _context.SaveChangesAsync() > 0;
    //     if (!result) return BadRequest("Failed to create product");

    //     return CreatedAtAction(nameof(GetProduct), new { product.Id }, newProduct);
    // }
}
