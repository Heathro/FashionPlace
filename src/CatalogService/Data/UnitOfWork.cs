using AutoMapper;
using CatalogService.Interfaces;

namespace CatalogService.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(CatalogDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IProductsRepository Products => new ProductsRepository(_context, _mapper);
    public IBrandsRepository Brands => new BrandsRepository(_context);
    public ICategoriesRepository Categories => new CategoriesRepository(_context);
    public IColorsRepository Colors => new ColorsRepository(_context);
    public ISizesRepository Sizes => new SizesRepository(_context);
    public ISpecificationTypesRepository SpecificationTypes => new SpecificationTypesRepository(_context);
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
