namespace business.Entities;

public class CreateVariant
{
    public Guid Id { get; set; }
    public ICollection<string> Colors { get; set; }
    public string Color { get; set; }
    public ICollection<string> Sizes { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}
