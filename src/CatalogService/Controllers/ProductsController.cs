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

    private List<string> GetCategories(Category category)
    {
        var categoryList = new List<string>();
        while (category != null)
        {
            categoryList.Add(category.Name);
            category = category.ParentCategory;
        }
        return categoryList;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == createProductDto.Brand);
        var model = new Model
        {
            Name = createProductDto.Model,
            Brand = brand ?? new Brand { Name = createProductDto.Brand }
        };

        var productCategories = new List<ProductCategory>();
        foreach (var category in createProductDto.Categories)
        {
            Category parent = null;
            if (category.ParentCategoryId != null)
            {
                parent = await _context.Categories
                    .Include(c => c.ParentCategory)
                        .ThenInclude(c => c.ParentCategory)
                            .ThenInclude(c => c.ParentCategory)
                                .ThenInclude(c => c.ParentCategory)
                    .FirstOrDefaultAsync(c => c.Id == category.ParentCategoryId);
            }

            Category current = null;
            foreach (var newCategory in category.NewCategories)
            {
                current = new Category { Name = newCategory, ParentCategory = parent };
                parent = current;
            }

            productCategories.Add(new ProductCategory { Category = current ?? parent });
        }

        var variants = new List<Variant>();
        foreach (var variant in createProductDto.Variants)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(c => c.Name == variant.Color);
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Name == variant.Size);
            variants.Add(new Variant
            {
                Color = color ?? new Color { Name = variant.Color },
                Size = size ?? new Size { Name = variant.Size },
                Price = variant.Price,
                Discount = variant.Discount,
                Quantity = variant.Quantity,
                ImageUrl = variant.ImageUrl
            });
        }

        var specifications = new List<Specification>();
        foreach (var specification in createProductDto.Specifications)
        {
            var type = await _context.SpecificationTypes.FirstOrDefaultAsync(s => s.Type == specification.Type);
            specifications.Add(new Specification
            {
                SpecificationType = type ?? new SpecificationType { Type = specification.Type },
                Value = specification.Value
            });
        }

        var product = new Product()
        {
            Description = createProductDto.Description,
            Model = model,
            ProductCategories = productCategories,
            Variants = variants,
            Specifications = specifications
        };

        _context.Products.Add(product);

        var newProduct = _mapper.Map<ProductDto>(product);

        var productAdded = _mapper.Map<ProductAdded>(newProduct);
        await _publishEndpoint.Publish(productAdded);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Failed to create product");

        return CreatedAtAction(nameof(GetProduct), new { product.Id }, newProduct);
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
                Id = c.Id,
                Name = c.Name,
                SubCategories = BuildCategoryHierarchy(c.Id, categories)
            })
            .ToList();
    }
}
