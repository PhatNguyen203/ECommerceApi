using ECommerce.Api.Searchs.Entities;
using ECommerce.Api.Searchs.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory httpClient, ILogger<ProductsService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMsg)> GetProductsAsync()
        {
            try
            {
                var client = httpClient.CreateClient("ProductsService");
                var response = await client.GetAsync($"/api/products");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
