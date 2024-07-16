namespace business.Entities;

public class CreateCategory
{
    public Guid Id { get; set; }
    public ICollection<CategoryPicker> CategoryPickers { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string NewCategories { get; set; }
}
