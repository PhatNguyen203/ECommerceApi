using ECommerce.Api.Searchs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services.Contracts
{
    public interface IProductsService
    {
        Task<(Boolean isSuccess, IEnumerable<Product> products, string errorMsg)> GetProductsAsync();
    }
}
