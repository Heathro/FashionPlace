namespace CatalogService.DTOs;

public class CreateProductDto
{
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public ICollection<string> Category { get; set; }
    public ICollection<CreateVariantDto> Variants { get; set; }
    public ICollection<CreateSpecificationDto> Specifications { get; set; }
}
