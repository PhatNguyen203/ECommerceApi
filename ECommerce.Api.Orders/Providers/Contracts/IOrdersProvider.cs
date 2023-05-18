using ECommerce.Api.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers.Contracts
{
    public interface IOrdersProvider
    {
        Task<(bool isSuccess, IEnumerable<OrderDto> ordersDto, string errorMsg)> GetOrdersAsync(int customerId);
        Task<(bool isSuccess, OrderDto orderDto, string errorMsg)> GetOrderByIdAsync(int customerId, int orderId);
    }
}
