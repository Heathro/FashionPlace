using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/sizes")]
public class SizesController : ControllerBase
{
    private readonly CatalogDbContext _context;

    public SizesController(CatalogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetSizes()
    {
        var sizes = await _context.Sizes.Select(s => s.Name).ToListAsync();
        return Ok(sizes);
    }
}
