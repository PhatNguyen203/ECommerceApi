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
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory httpClient, ILogger<CustomersService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }
        public async Task<(bool isSuccess, Customer customer, string errorMsg)> GetCustomerAsync(int customerId)
        {
            try
            {
                var client = httpClient.CreateClient("CustomersService");
                var response = await client.GetAsync($"/api/customers/{customerId}");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex.ToString());
                return (false, null, ex.Message);
                
            }
        }
    }
}
