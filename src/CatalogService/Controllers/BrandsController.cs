using Microsoft.AspNetCore.Mvc;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/brands")]
public class BrandsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BrandsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        var brands = await _unitOfWork.Brands.GetBrandNamesAsync();
        return Ok(brands);
    }
}
