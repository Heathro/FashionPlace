using Microsoft.AspNetCore.Mvc;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/sizes")]
public class SizesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SizesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetSizes()
    {
        var sizes = await _unitOfWork.Sizes.GetSizeNamesAsync();
        return Ok(sizes);
    }
}
