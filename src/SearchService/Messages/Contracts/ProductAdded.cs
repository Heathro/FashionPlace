using SearchService.Entities;

namespace Contracts;

public class ProductAdded
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<Variant> Variants { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
