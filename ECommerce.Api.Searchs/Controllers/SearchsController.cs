using ECommerce.Api.Searchs.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchsController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchsController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await searchService.SearchASync(customerId);
            if (result.isSuccess)
                return Ok(result.searchResult);
            return NotFound();
        }
    }
}
