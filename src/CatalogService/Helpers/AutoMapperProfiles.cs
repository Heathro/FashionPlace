using AutoMapper;
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
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.Specifications, o => o.MapFrom
                (
                    s => s.Specifications.Select(p => p.SpecificationType.Name + ": " + p.Value).ToList()
                ));
    }
}