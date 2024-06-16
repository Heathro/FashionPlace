namespace CatalogService.Entities;

public class Size
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Variant> Variants { get; set; }
}
