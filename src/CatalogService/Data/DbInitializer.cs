using Microsoft.EntityFrameworkCore;
using CatalogService.Entities;

namespace CatalogService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        scope.ServiceProvider.GetService<CatalogDbContext>().Database.Migrate();
    }

    private static void SeedData(CatalogDbContext context)
    {
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
                        ImageUrl = "https://images.pexels.com/photos/1304647/pexels-photo-1304647.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Variant
                    {
                        Color = new Color { Name = "White" },
                        Size = new Size { Name = "M" },
                        Price = 10,
                        Discount = 0,
                        Quantity = 4,
                        ImageUrl = "https://images.pexels.com/photos/1304647/pexels-photo-1304647.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Variant
                    {
                        Color = new Color { Name = "Black" },
                        Size = new Size { Name = "S" },
                        Price = 12,
                        Discount = 10,
                        Quantity = 3,
                        ImageUrl = "https://images.pexels.com/photos/3399995/pexels-photo-3399995.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },                    
                    new Variant
                    {
                        Color = new Color { Name = "Black" },
                        Size = new Size { Name = "L" },
                        Price = 12,
                        Discount = 10,
                        Quantity = 7,
                        ImageUrl = "https://images.pexels.com/photos/3399995/pexels-photo-3399995.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
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
                        Color = new Color { Name = "Gold" },
                        Size = new Size { Name = "8 UK" },
                        Price = 40,
                        Discount = 30,
                        Quantity = 2,
                        ImageUrl = "https://images.pexels.com/photos/1537671/pexels-photo-1537671.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                    },
                    new Variant
                    {
                        Color = new Color { Name = "Gold" },
                        Size = new Size { Name = "7 UK" },
                        Price = 40,
                        Discount = 30,
                        Quantity = 5,
                        ImageUrl = "https://images.pexels.com/photos/1537671/pexels-photo-1537671.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
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
