using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;
using CatalogService.DTOs;
using CatalogService.Entities;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/categories")]
public class CategoriesController : ControllerBase
{
    private readonly CatalogDbContext _context;

    public CategoriesController(CatalogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
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
