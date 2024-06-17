namespace CatalogService.DTOs;

public class CreateCategoryDto
{
    public ICollection<string> NewCategories { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
