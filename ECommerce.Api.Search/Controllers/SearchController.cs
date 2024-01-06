using ECommerce.Api.Search.DTOs;
using ECommerce.Api.Search.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTermDto searchTerm)
        {
            var result = await searchService.SearchAsync(searchTerm.CustomerId);

            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }

            return NotFound();
        }
    }
}
