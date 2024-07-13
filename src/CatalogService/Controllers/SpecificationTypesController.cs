using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.Data;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/specificationTypes")]
public class SpecificationTypesController : ControllerBase
{
    private readonly CatalogDbContext _context;

    public SpecificationTypesController(CatalogDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetSpecificationTypes()
    {
        var specificationTypes = await _context.SpecificationTypes.Select(s => s.Type).ToListAsync();
        return Ok(specificationTypes);
    }
}
