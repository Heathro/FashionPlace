using AutoMapper;
using Contracts;
using CatalogService.Entities;
using CatalogService.DTOs;

namespace CatalogService.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Model.Brand.Name))
            .ForMember(d => d.Model, o => o.MapFrom(s => s.Model.Name))
            .ForMember(d => d.Categories, o => o.MapFrom(s =>
                GetCategories(s.ProductCategories.Select(pc => pc.Category).ToList())));

        CreateMap<Variant, VariantDto>()
            .ForMember(d => d.Color, o => o.MapFrom(s => s.Color.Name))
            .ForMember(d => d.Size, o => o.MapFrom(s => s.Size.Name));

        CreateMap<Specification, SpecificationDto>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.SpecificationType.Type));

        CreateMap<ProductDto, ProductAdded>();
    }

    private List<List<string>> GetCategories(List<Category> categories)
    {
        var categoriesList = new List<List<string>>();

        foreach (var category in categories)
        {
            var categoryPointer = category;

            var categoryList = new List<string>();

            while (categoryPointer != null)
            {
                categoryList.Add(categoryPointer.Name);
                categoryPointer = categoryPointer.ParentCategory;
            }
            
            categoriesList.Add(categoryList);
        }
        
        return categoriesList;
    }
}