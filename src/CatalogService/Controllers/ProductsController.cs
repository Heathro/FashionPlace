using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MassTransit;
using AutoMapper;
using Contracts;
using CatalogService.Data;
using CatalogService.DTOs;
using CatalogService.Entities;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/products")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        return await _unitOfWork.Products.GetProductsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var product = await _unitOfWork.Products.GetProductAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        var brand = await _unitOfWork.Brands.GetBrandAsync(createProductDto.Brand);
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
                parent = await _unitOfWork.Categories.GetCategoryAsync(category.ParentCategoryId);
            }
            Category current = null;
            if (category.NewCategories != null)
            {
                foreach (var newCategory in category.NewCategories)
                {
                    current = new Category { Name = newCategory, ParentCategory = parent };
                    parent = current;
                }
            }            
            productCategories.Add(new ProductCategory { Category = current ?? parent });
        }

        var variants = new List<Variant>();
        foreach (var variant in createProductDto.Variants)
        {
            var color = await _unitOfWork.Colors.GetColorAsync(variant.Color);
            if (color == null)
            {
                color = new Color { Name = variant.Color };
                _unitOfWork.Colors.AddColor(color);
                await _unitOfWork.SaveChangesAsync();
            }

            var size = await _unitOfWork.Sizes.GetSizeAsync(variant.Size);
            if (size == null)
            {
                size = new Size { Name = variant.Size };
                _unitOfWork.Sizes.AddSize(size);
                await _unitOfWork.SaveChangesAsync();
            }

            variants.Add(new Variant
            {
                Color = color,
                Size = size,
                Price = variant.Price,
                Discount = variant.Discount,
                Quantity = variant.Quantity,
                ImageUrl = variant.ImageUrl
            });
        }

        var specifications = new List<Specification>();
        if (createProductDto.Specifications != null)
        {
            foreach (var specification in createProductDto.Specifications)
            {
                var type = await _unitOfWork.SpecificationTypes.GetSpecificationTypeAsync(specification.Type);
                specifications.Add(new Specification
                {
                    SpecificationType = type ?? new SpecificationType { Type = specification.Type },
                    Value = specification.Value
                });
            }
        }

        var product = new Product()
        {
            Description = createProductDto.Description,
            Model = model,
            ProductCategories = productCategories,
            Variants = variants,
            Specifications = specifications
        };

        _unitOfWork.Products.AddProduct(product);

        var productDto = _mapper.Map<ProductDto>(product);

        var productAdded = _mapper.Map<ProductAdded>(productDto);
        await _publishEndpoint.Publish(productAdded);

        var result = await _unitOfWork.SaveChangesAsync();
        if (!result) return BadRequest("Failed to create product");

        return CreatedAtAction(nameof(GetProduct), new { product.Id }, productDto);
    }
}
