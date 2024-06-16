namespace CatalogService.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public ICollection<ProductCategoryDto> ProductCategories { get; set; }
    public ICollection<VariantDto> Variants { get; set; }
    public ICollection<SpecificationDto> Specifications { get; set; }
}