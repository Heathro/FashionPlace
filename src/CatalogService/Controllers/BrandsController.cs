using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/brands")]
public class BrandsController : ControllerBase
{
    private readonly CatalogDbContext _context;

    public BrandsController(CatalogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        var brands = await _context.Brands.Select(b => b.Name).ToListAsync();
        return Ok(brands);
    }
}
