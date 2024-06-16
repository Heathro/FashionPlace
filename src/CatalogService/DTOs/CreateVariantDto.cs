namespace CatalogService.DTOs;

public class CreateVariantDto
{
    public string Color { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}
