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
            Console.WriteLine("======>>>>>> No seeding needed. Database already contains data.");
            return;
        }

        Console.WriteLine("======>>>>>> Seeding data.");

        var products = new List<Product>()
        {
            new Product()
            {
                ProductId = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                ImageUrl = "https://cdn.pixabay.com/photo/2024/05/25/05/34/ai-generated-8786361_1280.jpg",
                Description = "very nice t-shirt",
                Price = 10,
                Quantity = 4,
                Model = new Model()
                {
                    ModelId = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                    Name = "Comfy",
                    Brand = new Brand()
                    {
                        BrandId = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                        Name = "Adidas"
                    }
                },
                Category = new Category()
                {
                    CategoryId = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                    Name = "T-Shirts",
                    Description = "all t-shirts"
                },
                Specifications = new List<Specification>()
                {
                    new Specification()
                    {
                        SpecificationId = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                        Value = "White",
                        SpecificationType = new SpecificationType()
                        {
                            SpecificationTypeId = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                            Name = "Color"
                        }
                    }
                }
            },
            new Product()
            {
                ProductId = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                ImageUrl = "https://cdn.pixabay.com/photo/2017/08/02/00/28/people-2569043_1280.jpg",
                Description = "cool rollneck",
                Price = 5,
                Quantity = 2,
                Model = new Model()
                {
                    ModelId = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                    Name = "Skinny",
                    Brand = new Brand()
                    {
                        BrandId = Guid.Parse("f2a8a1c2-4c5e-4f42-9a97-72639bcb6578"),
                        Name = "Nike"
                    }
                },
                Category = new Category()
                {
                    CategoryId = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                    Name = "Rollneck",
                    Description = "all rollnecks"
                },
                Specifications = new List<Specification>()
                {
                    new Specification()
                    {
                        SpecificationId = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                        Value = "Black",
                        SpecificationType = new SpecificationType()
                        {
                            SpecificationTypeId = Guid.Parse("0c0c0d9e-8b9e-4b9d-8b9f-7b9e8b9e8b9f"),
                            Name = "Color"
                        }
                    }
                }
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
