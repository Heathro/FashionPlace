namespace CatalogService.Entities;

public class SpecificationType
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public ICollection<Specification> Specifications { get; set; }
}
