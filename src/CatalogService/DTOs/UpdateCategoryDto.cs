using CatalogService.Helpers;
using CatalogService.Interfaces;

namespace CatalogService.DTOs;

public class UpdateCategoryDto : INewCategoryDto
{   
    public Guid? Id { get; set; }
    [NewCategoriesValidation]
    public ICollection<string> NewCategories { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
