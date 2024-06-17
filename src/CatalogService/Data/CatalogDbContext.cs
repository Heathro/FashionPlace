using Microsoft.EntityFrameworkCore;
using MassTransit;
using CatalogService.Entities;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Color> Colors { get; set; }

    public DbSet<Specification> Specifications { get; set; }
    public DbSet<SpecificationType> SpecificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Model)
            .WithOne(m => m.Product)
            .HasForeignKey<Product>(p => p.ModelId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Model>()
            .HasOne(m => m.Brand)
            .WithMany(b => b.Models)
            .HasForeignKey(m => m.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });        
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Category>()
            .HasMany(c => c.SubCategories)
            .WithOne(c => c.ParentCategory)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Color>()
            .HasMany(c => c.Variants)
            .WithOne(v => v.Color)
            .HasForeignKey(v => v.ColorId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Size>()
            .HasMany(s => s.Variants)
            .WithOne(v => v.Size)
            .HasForeignKey(v => v.SizeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Specification>()
            .HasOne(s => s.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Specification>()
            .HasOne(s => s.SpecificationType)
            .WithMany(st => st.Specifications)
            .HasForeignKey(s => s.SpecificationTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
