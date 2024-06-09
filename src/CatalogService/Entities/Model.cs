namespace CatalogService.Entities;

public class Model
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
    public Product Product { get; set; }
}
