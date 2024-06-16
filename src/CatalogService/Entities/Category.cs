namespace CatalogService.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }    
    public ICollection<Category> SubCategories { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}
