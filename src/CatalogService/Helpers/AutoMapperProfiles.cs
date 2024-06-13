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
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.Specifications, o => o.MapFrom(s => s.Specifications
                .ToDictionary(sp => sp.SpecificationType.Name, sp => sp.Value)
            ));

        CreateMap<CreateProductDto, Product>()
            .ForMember(d => d.Model, o => o.MapFrom(s => 
                new Model{ Name = s.Model, Brand = new Brand { Name = s.Brand } }
            ))
            .ForMember(d => d.Category, o => o.MapFrom(s =>
                new Category { Name = s.Category }
            ))
            .ForMember(d => d.Specifications, o => o.MapFrom(s =>
                s.Specifications.Select(p => new Specification
                {
                    Value = p.Value,
                    SpecificationType = new SpecificationType { Name = p.Key }
                })
            ));

        CreateMap<ProductDto, ProductAdded>();
    }
}