namespace CatalogService.Entities;

public class SpecificationType
{
    public Guid SpecificationTypeId { get; set; }
    public string Name { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
