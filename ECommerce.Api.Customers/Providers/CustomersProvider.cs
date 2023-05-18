using AutoMapper;
using ECommerce.Api.Customers.Data;
using ECommerce.Api.Customers.Dtos;
using ECommerce.Api.Customers.Entities;
using ECommerce.Api.Customers.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            //seeding Data
            SeedData();
        }

        public async Task<(bool isSuccess, CustomerDto customerDto, string errorMsg)> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

                if(customer != null)
                {
                    var customerDto = mapper.Map<Customer, CustomerDto>(customer);
                    return (true, customerDto, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, IEnumerable<CustomerDto> customersDto, string errorMsg)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null)
                {
                    var customersDto = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
                    return (true, customersDto, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Test", Address = "Test Address" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "Test2", Address = "Test Address 2" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "Test2", Address = "Test Address 3" });
                dbContext.SaveChanges();
            }
        }
    }
}
