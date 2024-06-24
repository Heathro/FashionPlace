using AutoMapper;
using Contracts;
using SearchService.Entities;

namespace SearchService.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ProductAdded, Product>()
            .ForMember(d => d.DiscountAmountHighest, o => o.MapFrom(s => GetHighestDiscountAmount(s)))
            .ForMember(d => d.DiscountPercentHighest, o => o.MapFrom(s => GetHighestDiscountPercent(s)))
            .ForMember(d => d.DiscountedPriceLowest, o => o.MapFrom(s => GetLowestDiscountPrice(s)))
            .ForMember(d => d.DiscountedPriceHighest, o => o.MapFrom(s => GetHighestDiscountPrice(s)))
            .ForMember(d => d.SearchString, o => o.MapFrom(s => GetSearchString(s)));
    }

    private static DiscountData GetHighestDiscountAmount(ProductAdded product)
    {
        Guid id = Guid.Empty;
        decimal max = 0m;

        foreach (var variant in product.Variants)
        {
            var discountAmount = variant.Price * (variant.Discount / 100.0m);
            if (discountAmount > max)
            {
                max = discountAmount;
                id = variant.Id;
            }
        }

        return new DiscountData { Id = id, Value = max };
    }

    private static DiscountData GetHighestDiscountPercent(ProductAdded product)
    {
        Guid id = Guid.Empty;
        decimal max = 0m;

        foreach (var variant in product.Variants)
        {
            if (variant.Discount > max)
            {
                max = variant.Discount;
                id = variant.Id;
            }
        }

        return new DiscountData { Id = id, Value = max };
    }

    private static DiscountData GetLowestDiscountPrice(ProductAdded product)
    {
        Guid id = Guid.Empty;
        decimal min = decimal.MaxValue;

        foreach (var variant in product.Variants)
        {
            var discountedPrice = variant.Price * (1 - variant.Discount / 100.0m);
            if (discountedPrice < min)
            {
                min = discountedPrice;
                id = variant.Id;
            }
        }

        return new DiscountData { Id = id, Value = min };
    }

    private static DiscountData GetHighestDiscountPrice(ProductAdded product)
    {
        Guid id = Guid.Empty;
        decimal max = 0m;

        foreach (var variant in product.Variants)
        {
            var discountedPrice = variant.Price * (1 - variant.Discount / 100.0m);
            if (discountedPrice > max)
            {
                max = discountedPrice;
                id = variant.Id;
            }
        }

        return new DiscountData { Id = id, Value = max };
    }

    private static string GetSearchString(ProductAdded product)
    {
        var searchStringList = new List<string>();

        foreach (var productCategory in product.ProductCategories)
        {
            searchStringList.AddRange(productCategory.Categories);
        }

        searchStringList.AddRange(product.Variants.Select(v => v.Color));
        searchStringList.AddRange(product.Variants.Select(v => v.Size));
        searchStringList.AddRange(product.Specifications.Select(s => s.Value));

        return string.Join(" ", searchStringList);
    }
}
