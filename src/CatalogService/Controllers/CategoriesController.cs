using Microsoft.AspNetCore.Mvc;
using CatalogService.DTOs;
using CatalogService.Entities;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await _unitOfWork.Categories.GetCategoriesAsync();
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
