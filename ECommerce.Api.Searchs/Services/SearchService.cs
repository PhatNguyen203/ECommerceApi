using ECommerce.Api.Searchs.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Searchs.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool isSuccess, dynamic searchResult)> SearchASync(int customerId)
        {
            var orders = await ordersService.GetOrdersAsync(customerId);
            var customer = await customersService.GetCustomerAsync(customerId);
            var products = await productsService.GetProductsAsync();

            if(orders.isSuccess)
            {

                foreach(var order in orders.orders)
                {
                    foreach(var item in order.OrderItems)
                    {
                        item.ProductName = products.isSuccess ? products.products.FirstOrDefault(x => x.Id == item.ProductId).Name : "Product Name is not available";
                    }
                }    
                   
                var result = new 
                {
                    Customer = customer.isSuccess ? customer.customer : new Entities.Customer { Name = "Customer is not available" },
                    Orders = orders.orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
