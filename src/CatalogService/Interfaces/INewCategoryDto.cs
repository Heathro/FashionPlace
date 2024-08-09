namespace CatalogService.Interfaces;

public interface INewCategoryDto
{
    public ICollection<string> NewCategories { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
