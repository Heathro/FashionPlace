using Microsoft.AspNetCore.Mvc;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/colors")]
public class ColorsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ColorsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetColors()
    {
        var colors = await _unitOfWork.Colors.GetColorNamesAsync();
        return Ok(colors);
    }
}
