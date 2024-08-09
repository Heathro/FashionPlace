using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.DTOs;

public class UpdateSpecificationDto
{
    [Required]
    public string Type { get; set; }
    [Required]
    public string Value { get; set; }
}
