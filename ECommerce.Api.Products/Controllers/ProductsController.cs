using ECommerce.Api.Products.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetAllAsync();
            if(result.isSuccess)
            {
                return Ok(result.productsDto);
            }
            return NotFound();
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId)
        {
            var result = await productsProvider.GetProductByIdAsync(productId);
            if(result.isSuccess)
            {
                return Ok(result.productDto);
            }
            return NotFound();
        }
    }
}
