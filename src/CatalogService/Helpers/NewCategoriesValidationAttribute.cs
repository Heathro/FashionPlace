using System.ComponentModel.DataAnnotations;
using CatalogService.DTOs;

namespace CatalogService.Helpers;

public class NewCategoriesValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var categoryDto = (CreateCategoryDto)validationContext.ObjectInstance;

        if (categoryDto.ParentCategoryId == null)
        {  
            if (categoryDto.NewCategories == null || categoryDto.NewCategories.Count == 0)
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