namespace CatalogService.Entities;

public class Color
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Variant> Variants { get; set; }
}
