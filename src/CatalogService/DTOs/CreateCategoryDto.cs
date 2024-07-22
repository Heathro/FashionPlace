using CatalogService.Helpers;

namespace CatalogService.DTOs;

public class CreateCategoryDto
{
    [NewCategoriesValidation]
    public ICollection<string> NewCategories { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
