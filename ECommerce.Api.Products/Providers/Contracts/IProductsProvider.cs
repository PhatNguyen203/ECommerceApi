using ECommerce.Api.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers.Contracts
{
    public interface IProductsProvider
    {
        Task<(Boolean isSuccess, IEnumerable<ProductDto> productsDto, string errorMsg)> GetAllAsync();
        Task<(Boolean isSuccess, ProductDto productDto, string errorMsg)> GetProductByIdAsync(int productId);

    }
}
