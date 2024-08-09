using System.ComponentModel.DataAnnotations;
using CatalogService.Interfaces;

namespace CatalogService.Helpers;

public class NewCategoriesValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var newCategoryDto = (INewCategoryDto)validationContext.ObjectInstance;

        if (newCategoryDto.ParentCategoryId == null)
        {  
            if (newCategoryDto.NewCategories == null || newCategoryDto.NewCategories.Count == 0)
            {
                return new ValidationResult
                (
                    "NewCategories should contain at least one item if ParentCategoryId is null."
                );
            }            
        }

        return ValidationResult.Success;
    }
}
