namespace CatalogService.Entities;

public class Variant
{
    public Guid Id { get; set; }
    public Guid ColorId { get; set; }
    public Color Color { get; set; }
    public Guid SizeId { get; set; }
    public Size Size { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
