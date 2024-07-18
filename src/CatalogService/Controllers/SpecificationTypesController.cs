using Microsoft.AspNetCore.Mvc;
using CatalogService.Interfaces;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/catalog/specificationTypes")]
public class SpecificationTypesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SpecificationTypesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetSpecificationTypes()
    {
        var specificationTypes = await _unitOfWork.SpecificationTypes.GetSpecificationTypeNamesAsync();
        return Ok(specificationTypes);
    }
}
