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
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<OrdersService> logger;

        public OrdersService(IHttpClientFactory httpClient, ILogger<OrdersService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Order> orders, string errorMsg)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = httpClient.CreateClient("OrdersService");
                var response = await client.GetAsync($"/api/orders/{customerId}");

                if(response.IsSuccessStatusCode)
                {
                    //deserialize the response
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }; // add name case sensitive prop for json
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

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
