using ECommerce.Api.Orders.Providers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);

            if(result.isSuccess)
            {
                return Ok(result.ordersDto);
            }
            return NotFound(result.errorMsg);
        }
        [HttpGet("{customerId}/{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int customerId, int orderId)
        {
            var result = await ordersProvider.GetOrderByIdAsync(customerId, orderId);

            if (result.isSuccess)
            {
                return Ok(result.orderDto);
            }
            return NotFound(result.errorMsg);
        }
    }
}
