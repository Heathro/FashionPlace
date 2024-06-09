namespace CatalogService.Entities;

public class SpecificationType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
