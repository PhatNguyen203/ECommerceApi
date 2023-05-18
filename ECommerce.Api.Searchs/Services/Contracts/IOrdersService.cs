using ECommerce.Api.Searchs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services.Contracts
{
    public interface IOrdersService
    {
        Task<(Boolean isSuccess, IEnumerable<Order> orders, string errorMsg)> GetOrdersAsync(int customerId); 
    }
}
