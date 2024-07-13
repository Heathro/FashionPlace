namespace business.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<CategoryDto> SubCategories { get; set; }
}
