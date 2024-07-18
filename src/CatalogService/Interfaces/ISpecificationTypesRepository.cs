using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface ISpecificationTypesRepository
{
    Task<List<string>> GetSpecificationTypeNamesAsync();
    Task<SpecificationType> GetSpecificationTypeAsync(string type);
}
