namespace CatalogService.Entities;

public class Specification
{
    public Guid SpecificationId { get; set; }
    public string Value { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid SpecificationTypeId { get; set; }
    public SpecificationType SpecificationType { get; set; }
}
