using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs;

public class CreateVariantDto
{
    [Required]
    public string Color { get; set; }
    [Required]
    public string Size { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal Discount { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public string ImageUrl { get; set; }
}
