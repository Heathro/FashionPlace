namespace business.Entities;

public class CreateSpecification
{
    public Guid Id { get; set; }
    public ICollection<string> Types { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
}
