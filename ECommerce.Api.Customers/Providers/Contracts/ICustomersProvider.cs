using ECommerce.Api.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers.Contracts
{
    public interface ICustomersProvider
    {
        Task<(Boolean isSuccess, IEnumerable<CustomerDto> customersDto, string errorMsg)> GetCustomersAsync();
        Task<(Boolean isSuccess, CustomerDto customerDto, string errorMsg)> GetCustomerByIdAsync(int customerId);
    }
}
