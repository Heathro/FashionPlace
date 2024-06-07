namespace CatalogService.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid ModelId { get; set; }
    public Model Model { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
