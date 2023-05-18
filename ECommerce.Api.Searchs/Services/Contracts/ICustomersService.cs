using ECommerce.Api.Searchs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services.Contracts
{
    public interface ICustomersService
    {
        Task<(Boolean isSuccess, Customer customer, string errorMsg)> GetCustomerAsync(int customerId);
    }
}
