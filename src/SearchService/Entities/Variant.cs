namespace SearchService.Entities;

public class Variant
{
    public Guid Id { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }
}
