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
    public DiscountData DiscountAmountHighest { get; set; }
    public DiscountData DiscountPercentHighest { get; set; }
    public DiscountData DiscountedPriceLowest { get; set; }
    public DiscountData DiscountedPriceHighest { get; set; }
    public string SearchString { get; set; }
}
