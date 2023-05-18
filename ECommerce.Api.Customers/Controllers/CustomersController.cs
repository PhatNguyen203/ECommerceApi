using ECommerce.Api.Customers.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customerProvider;

        public CustomersController(ICustomersProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customerProvider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.customersDto);
            }
            return NotFound();
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetProductByIdAsync(int customerId)
        {
            var result = await customerProvider.GetCustomerByIdAsync(customerId);
            if (result.isSuccess)
            {
                return Ok(result.customerDto);
            }
            return NotFound();
        }
    }
}
