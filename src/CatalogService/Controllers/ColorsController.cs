using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/colors")]
public class ColorsController : ControllerBase
{
    private readonly CatalogDbContext _context;

    public ColorsController(CatalogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetColors()
    {
        var colors = await _context.Colors.Select(c => c.Name).ToListAsync();
        return Ok(colors);
    }
}
