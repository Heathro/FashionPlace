using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using business.DTOs;
using business.Entities;

namespace business;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<string> Brands { get; set; }
    public string Brand { get; set; }

    private readonly List<CategoryPicker> _categoryPickers;
    public ObservableCollection<CreateCategory> CreateCategories { get; set; }
    public ICommand AddCategoryCommand { get; }

    private readonly List<string> _colors;
    private readonly List<string> _sizes;
    public ObservableCollection<CreateVariant> CreateVariants { get; set; }
    public ICommand AddVariantCommand { get; }

    
    private readonly List<string> _specificationTypes;
    public ObservableCollection<CreateSpecification> CreateSpecifications { get; set; }
    public ICommand AddSpecificationCommand { get; }


    public MainViewModel
    (
        List<CategoryDto> categories,
        List<string> brands,
        List<string> colors,
        List<string> sizes,
        List<string> specificationTypes
    )
    {
        Brands = new ObservableCollection<string>(brands);
        Brands.Insert(0, "(New)");
        Brand = "";

        _colors = colors;
        _colors.Insert(0, "(New)");
        _sizes = sizes;
        _sizes.Insert(0, "(New)");
        CreateVariants = new ObservableCollection<CreateVariant>();
        AddVariantCommand = new Command(AddVariant);
        AddVariant();

        _categoryPickers = ConvertToCategoryPicker(categories);
        CreateCategories = new ObservableCollection<CreateCategory>();
        AddCategoryCommand = new Command(AddCategory);
        AddCategory();

        _specificationTypes = specificationTypes;
        _specificationTypes.Insert(0, "(New)");
        CreateSpecifications = new ObservableCollection<CreateSpecification>();
        AddSpecificationCommand = new Command(AddSpecification);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddCategory()
    {
        CreateCategories.Add
        (
            new CreateCategory
            {
                Id = Guid.NewGuid(),
                CategoryPickers = _categoryPickers,
                ParentCategoryId = null,
                NewCategories = ""
            }
        );
    }

    public void DeleteCategory(Guid Id)
    {
        var category = CreateCategories.FirstOrDefault(c => c.Id == Id);
        if (category != null)
        {
            CreateCategories.Remove(category);
        }
    }
    
    public void AddVariant()
    {
        CreateVariants.Add
        (
            new CreateVariant
            {
                Id = Guid.NewGuid(),
                Colors = _colors,
                Color = "",
                Sizes = _sizes,
                Size = "",
                Price = 0,
                Discount = 0,
                Quantity = 1,
                ImageUrl = ""
            }
        );
    }

    public void DeleteVariant(Guid Id)
    {
        var variant = CreateVariants.FirstOrDefault(v => v.Id == Id);
        if (variant != null)
        {
            CreateVariants.Remove(variant);
        }
    }

    public void AddSpecification()
    {
        CreateSpecifications.Add
        (
            new CreateSpecification
            {
                Id = Guid.NewGuid(),
                Types = _specificationTypes,
                Type = "",
                Value = ""
            }
        );
    }

    public void DeleteSpecification(Guid Id)
    {
        var specification = CreateSpecifications.FirstOrDefault(v => v.Id == Id);
        if (specification != null)
        {
            CreateSpecifications.Remove(specification);
        }
    }

    public static List<CategoryPicker> ConvertToCategoryPicker(List<CategoryDto> categories)
    {
        List<CategoryPicker> result = [new CategoryPicker { Name = "(None)" }];
        foreach (var category in categories)
        {
            result.Add
            (
                new CategoryPicker
                {
                    Id = category.Id,
                    Name = category.Name
                }
            );
            AddSubCategories(category.SubCategories, result, 1);
        }
        return result;
    }

    private static void AddSubCategories(ICollection<CategoryDto> subCategories, List<CategoryPicker> result, int depth)
    {
        foreach (var subCategory in subCategories)
        {
            result.Add
            (
                new CategoryPicker
                {
                    Id = subCategory.Id,
                    Name = $"{new string(' ', depth * 4)}{new string('-', depth)} {subCategory.Name}"
                }
            );
            AddSubCategories(subCategory.SubCategories, result, depth + 1);
        }
    }
}
