using CatalogService.Entities;

namespace CatalogService.Interfaces;

public interface IColorsRepository
{
    Task<List<string>> GetColorNamesAsync();
    Task<Color> GetColorAsync(string name);
    void AddColor(Color color);
}
