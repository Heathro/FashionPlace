using Microsoft.EntityFrameworkCore;
using CatalogService.Entities;

namespace CatalogService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<CatalogDbContext>());
    }

    private static void SeedData(CatalogDbContext context)
    {
        context.Database.Migrate();

        if (context.Products.Any())
        {
            Console.WriteLine("\n\n\n======>>>>>> No seeding needed. Database already contains data.\n\n\n");
            return;
        }
        Console.WriteLine("\n\n\n======>>>>>> Seeding data.\n\n\n");

        var products = new List<Product>()
        {
            new Product()
            {
                Description = "very comfy",
                Model = new Model
                {
                    Name = "Strike",
                    Brand = new Brand { Name = "Adidas" }
                },
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "T-shirts",
                            ParentCategory = new Category
                            {
                                Name = "Clothing",
                                ParentCategory = new Category
                                {
                                    Name = "Mens",
                                    ParentCategory = null
                                }
                            }
                        }
                    },
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "Football",
                            ParentCategory = new Category
                            {
                                Name = "Sport",
                                ParentCategory = null
                            }
                        }
                    }
                },
                Variants = new List<Variant>
                {
                    new Variant
                    {
                        Color = new Color { Name = "White" },
                        Size = new Size { Name = "S" },
                        Price = 10,
                        Discount = 0,
                        Quantity = 8,
                        ImageUrl = "imageUrl adidas strike white s"
                    },
                    new Variant
                    {
                        Color = new Color { Name = "Black" },
                        Size = new Size { Name = "XL" },
                        Price = 12,
                        Discount = 10,
                        Quantity = 4,
                        ImageUrl = "imageUrl adidas strike black xl"
                    }
                },
                Specifications = new List<Specification>
                {
                    new Specification
                    {
                        SpecificationType = new SpecificationType{ Type = "Fabric" },
                        Value = "Polyester"
                    },
                    new Specification
                    {
                        SpecificationType = new SpecificationType{ Type = "Length" },
                        Value = "Regular"
                    }
                }
            },
            new Product
            {
                Description = "speedy runners",
                Model = new Model
                {
                    Name = "Air",
                    Brand = new Brand { Name = "Nike" }
                },
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory
                    {
                        Category = new Category
                        {
                            Name = "Trainers",
                            ParentCategory = new Category
                            {
                                Name = "Footwear",
                                ParentCategory = new Category
                                {
                                    Name = "Womens",
                                    ParentCategory = null
                                }
                            }
                        }
                    }
                },
                Variants = new List<Variant>
                {
                    new Variant
                    {
                        Color = new Color { Name = "Blue" },
                        Size = new Size { Name = "M" },
                        Price = 40,
                        Discount = 30,
                        Quantity = 2,
                        ImageUrl = "imageUrl nike air blue m"
                    }
                },
                Specifications = new List<Specification>
                {
                    new Specification
                    {
                        SpecificationType = new SpecificationType{ Type = "Surface" },
                        Value = "Road"
                    },
                    new Specification
                    {
                        SpecificationType = new SpecificationType{ Type = "Sole" },
                        Value = "Rubber"
                    }
                }
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
