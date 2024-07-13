using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using business.DTOs;
using business.Entities;

namespace business;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly List<CategoryPicker> _categoryPickers;
    public ObservableCollection<CreateCategory> CreateCategories { get; set; }
    public ICommand AddNewCategoryCommand { get; }

    public MainViewModel(List<CategoryDto> categories)
    {
        _categoryPickers = ConvertToCategoryPicker(categories);
        CreateCategories = new ObservableCollection<CreateCategory>();
        AddNewCategoryCommand = new Command(AddNewCategory);
        AddNewCategory();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddNewCategory()
    {
        CreateCategories.Add
        (
            new CreateCategory
            {
                CategoryPickers = _categoryPickers,
                ParentCategoryId = null,
                NewCategories = ""
            }
        );
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
