using MongoDB.Entities;

namespace SearchService.Entities;

public class Product : Entity
{
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
    public ICollection<Variant> Variants { get; set; }
    public ICollection<Specification> Specifications { get; set; }
    public string SearchString { get; set; }
}
