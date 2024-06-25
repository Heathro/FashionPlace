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
    public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery]SearchParams searchParams)
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
            "discount-amount" => query.Sort(p => p.Descending(p => p.DiscountAmountHighest.Value)),
            "discount-percent" => query.Sort(p => p.Descending(p => p.DiscountPercentHighest.Value)),
            "price-asc" => query.Sort(p => p.Ascending(p => p.DiscountedPriceLowest.Value)),
            "price-desc" => query.Sort(p => p.Descending(p => p.DiscountedPriceHighest.Value)),
            "brand-asc" => query.Sort(p => p.Ascending(p => p.Brand)),
            "brand-desc" => query.Sort(p => p.Descending(p => p.Brand)),
            _ => query.Sort(p => p.Ascending(p => p.DiscountedPriceLowest.Value))
        };

        if (!string.IsNullOrEmpty(searchParams.FilterBy))
        query = query.Match(p => p.Variants.Any(v => v.Color == searchParams.FilterBy));

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}
