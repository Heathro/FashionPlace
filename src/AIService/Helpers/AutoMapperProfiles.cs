using AutoMapper;
using Contracts;
using AIService.DTOs;
using AIService.Entities;

namespace AIService.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<MessageThread, MessageThreadDto>();
        CreateMap<Message, MessageDto>();

        CreateMap<MessageThreadDto, ModelChatRequest>();
        CreateMap<MessageDto, ModelChatMessage>()
            .ForMember(d => d.role, o => o.MapFrom(s => s.IsUser ? "user" : "assistant"));

        CreateMap<ProductAdded, Product>()
            .ForMember(d => d.Categories, o => o.MapFrom(s => GetCategories(s.ProductCategories)))
            .ForMember(d => d.Colors, o => o.MapFrom(s => GetColors(s.Variants)))
            .ForMember(d => d.Sizes, o => o.MapFrom(s => GetSizes(s.Variants)))
            .ForMember(d => d.Specifications, o => o.MapFrom(s => GetSpecifications(s.Specifications)));
    }

    private string GetCategories(ICollection<ProductCategory> productCategories)
    {
        HashSet<string> categories = new();
        foreach (var productCategory in productCategories)
        {
            foreach (var category in productCategory.Categories)
            {
                categories.Add(category);
            }
        }
        return string.Join(',', categories);
    }

    private string GetColors(ICollection<Variant> variants)
    {
        HashSet<string> colors = new();
        foreach (var variant in variants)
        {
            colors.Add(variant.Color);
        }
        return string.Join(',', colors);
    }

    private string GetSizes(ICollection<Variant> variants)
    {
        HashSet<string> sizes = new();
        foreach (var variant in variants)
        {
            sizes.Add(variant.Size);
        }
        return string.Join(',', sizes);
    }

    private string GetSpecifications(ICollection<Specification> specifications)
    {
        string specs = "";
        foreach (var specification in specifications)
        {
            specs += specification.Type + ":" + specification.Value + ",";
        }
        return specs.TrimEnd(',');
    }
}
