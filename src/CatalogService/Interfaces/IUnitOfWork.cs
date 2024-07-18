namespace CatalogService.Interfaces;

public interface IUnitOfWork
{
    public IProductsRepository Products { get; }
    public IBrandsRepository Brands { get; }
    public ICategoriesRepository Categories { get; }
    public IColorsRepository Colors { get; }
    public ISizesRepository Sizes { get; }
    public ISpecificationTypesRepository SpecificationTypes { get; }
    Task<bool> SaveChangesAsync();
}
