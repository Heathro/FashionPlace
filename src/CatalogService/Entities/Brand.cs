namespace CatalogService.Entities;

public class Brand
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public ICollection<Model> Models { get; set; }
}
