using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs;

public class CreateProductDto
{
    [Required]
    public string Description { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required][MinLength(1)]
    public ICollection<CreateCategoryDto> Categories { get; set; }
    [Required][MinLength(1)]
    public ICollection<CreateVariantDto> Variants { get; set; }
    public ICollection<CreateSpecificationDto> Specifications { get; set; }
}
