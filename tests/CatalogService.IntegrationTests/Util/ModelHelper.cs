using CatalogService.DTOs;
using CatalogService.Entities;

namespace CatalogService.IntegrationTests;

public static class ModelHelper
{
    public static CreateProductDto GetCreateProductDto()
    {
        return new CreateProductDto()
        {
            Description = "description-test",
            Brand = "brand-test",
            Model = "model-test",
            Categories = new List<CreateCategoryDto>()
            {
                new CreateCategoryDto()
                {
                    ParentCategoryId = null,
                    NewCategories = new List<string>()
                    {
                        "category-test"
                    }
                }
            },
            Variants = new List<CreateVariantDto>()
            {
                new CreateVariantDto()
                {
                    Color = "color-test",
                    Size = "size-test",
                    Price = 1,
                    Discount = 1,
                    Quantity = 1,
                    ImageUrl = "image-test"
                }
            },
            Specifications = new List<CreateSpecificationDto>()
            {
                new CreateSpecificationDto()
                {
                    Type = "type-test",
                    Value = "value-test"
                }
            }
        };
    }
    
    public static List<Product> GetProductsForTests()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = Guid.Parse("1a5e2d70-2b74-4d49-8f37-8bfa601d5d42"),
                Description = "description-test-1",
                Model = new Model
                {
                    Name = "model-test-1",
                    Brand = new Brand
                    {
                        Name = "brand-test-1"
                    }
                },
                ProductCategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "category-test-1"
                        }
                    }
                },
                Variants = new List<Variant>()
                {
                    new Variant
                    {
                        Color = new Color
                        {
                            Name = "color-test-1"
                        },
                        Size = new Size
                        {
                            Name = "size-test-1"
                        },
                        Price = 1,
                        Discount = 1,
                        Quantity = 1,
                        ImageUrl = "image-test-1"
                    }
                },
                Specifications = new List<Specification>()
                {
                    new Specification
                    {
                        SpecificationType = new SpecificationType
                        {
                            Type = "type-test-1"
                        },
                        Value = "value-test-1"
                    }
                }
            },
            new Product()
            {
                Id = Guid.Parse("3c8e7e7e-d1c9-4f6a-9d3f-2efc3e9487a5"),
                Description = "description-test-2",
                Model = new Model
                {
                    Name = "model-test-2",
                    Brand = new Brand
                    {
                        Name = "brand-test-2"
                    }
                },
                ProductCategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "category-test-2"
                        }
                    }
                },
                Variants = new List<Variant>()
                {
                    new Variant
                    {
                        Color = new Color
                        {
                            Name = "color-test-2"
                        },
                        Size = new Size
                        {
                            Name = "size-test-2"
                        },
                        Price = 2,
                        Discount = 2,
                        Quantity = 2,
                        ImageUrl = "image-test-2"
                    }
                },
                Specifications = new List<Specification>()
                {
                    new Specification
                    {
                        SpecificationType = new SpecificationType
                        {
                            Type = "type-test-2"
                        },
                        Value = "value-test-2"
                    }
                }
            },
            new Product()
            {
                Id = Guid.Parse("95d7687b-6406-4b3a-8b23-3eb0219a0b5e"),
                Description = "description-test-3",
                Model = new Model
                {
                    Name = "model-test-3",
                    Brand = new Brand
                    {
                        Name = "brand-test-3"
                    }
                },
                ProductCategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "category-test-3"
                        }
                    }
                },
                Variants = new List<Variant>()
                {
                    new Variant
                    {
                        Color = new Color
                        {
                            Name = "color-test-3"
                        },
                        Size = new Size
                        {
                            Name = "size-test-3"
                        },
                        Price = 3,
                        Discount = 3,
                        Quantity = 3,
                        ImageUrl = "image-test-3"
                    }
                },
                Specifications = new List<Specification>()
                {
                    new Specification
                    {
                        SpecificationType = new SpecificationType
                        {
                            Type = "type-test-3"
                        },
                        Value = "value-test-3"
                    }
                }
            }
        };
    }
}
