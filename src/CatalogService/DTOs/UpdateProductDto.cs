using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs;

public class UpdateProductDto
{
    [Required]
    public string Description { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required][MinLength(1)]
    public ICollection<UpdateCategoryDto> Categories { get; set; }
    [Required][MinLength(1)]
    public ICollection<UpdateVariantDto> Variants { get; set; }
    public ICollection<UpdateSpecificationDto> Specifications { get; set; }
}
