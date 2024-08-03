using MongoDB.Entities;

namespace AIService.Entities;

public class Product : Entity
{
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Categories { get; set; }
    public string Colors { get; set; }
    public string Sizes { get; set; }
    public string Specifications { get; set; }
}
