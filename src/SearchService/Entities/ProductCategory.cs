namespace SearchService.Entities;

public class ProductCategory
{
    public Guid Id { get; set; }
    public ICollection<string> Categories { get; set; }
}
