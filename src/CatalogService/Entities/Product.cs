namespace CatalogService.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Guid? ModelId { get; set; }
    public Model Model { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<Variant> Variants { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
