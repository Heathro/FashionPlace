using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs;

public class CreateSpecificationDto
{
    [Required]
    public string Type { get; set; }
    [Required]
    public string Value { get; set; }
}
