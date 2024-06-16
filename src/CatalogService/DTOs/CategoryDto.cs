namespace CatalogService.DTOs;

public class CategoryDto
{
    public string Name { get; set; }
    public List<CategoryDto> SubCategories { get; set; }
}
