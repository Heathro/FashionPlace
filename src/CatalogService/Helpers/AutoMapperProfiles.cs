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
            .ForMember(d => d.Model, o => o.MapFrom(s => s.Model.Name));

        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Category.Id))
            .ForMember(d => d.Categories, o => o.MapFrom(s => GetCategories(s.Category)));

        CreateMap<Variant, VariantDto>()
            .ForMember(d => d.Color, o => o.MapFrom(s => s.Color.Name))
            .ForMember(d => d.Size, o => o.MapFrom(s => s.Size.Name));

        CreateMap<Specification, SpecificationDto>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.SpecificationType.Type));

        CreateMap<ProductDto, ProductAdded>();
    }

    private List<string> GetCategories(Category category)
    {
        var categoryList = new List<string>();
        while (category != null)
        {
            categoryList.Insert(0, category.Name);
            category = category.ParentCategory;
        }        
        return categoryList;
    }
}