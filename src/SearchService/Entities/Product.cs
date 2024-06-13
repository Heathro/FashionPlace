using MongoDB.Entities;

namespace SearchService.Entities;

public class Product : Entity
{
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Category { get; set; }
    public IDictionary<string, string> Specifications { get; set; }
}
