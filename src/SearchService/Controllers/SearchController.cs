using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.Helpers;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Product, Product>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        query = searchParams.OrderBy switch
        {
            _ => query.Sort(x => x.Ascending(x => x.Brand))
        };

        query = searchParams.FilterBy switch
        {
            _ => query.Match(x => x.ID != null)
        };

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}