namespace NotificationService.DTOs;

public class ProductCategoryDto
{
    public Guid Id { get; set; }
    public ICollection<string> Categories { get; set; }
}
