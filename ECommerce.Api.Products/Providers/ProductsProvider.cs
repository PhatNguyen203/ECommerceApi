using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Dtos;
using ECommerce.Api.Products.Entities;
using ECommerce.Api.Products.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            //seeding data
            SeedData();
        }
        public async Task<(bool isSuccess, IEnumerable<ProductDto> productsDto, string errorMsg)> GetAllAsync()
        {
           try
           {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var productsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                    return (true, productsDto, null);
                }

                return (false, null, "Not Found");
           }
           catch( Exception ex)
           {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
           }
        }

        public async Task<(bool isSuccess, ProductDto productDto, string errorMsg)> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
                if(product != null)
                {
                    var productDto = mapper.Map<Product, ProductDto>(product);
                    return (true, productDto, null);
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
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Entities.Product(){Id = 1, Name = "Keyboard", Price = 19.99M, Inventory = 100});
                dbContext.Products.Add(new Entities.Product() { Id = 2, Name = "Mouse", Price = 9.99M, Inventory = 100 });
                dbContext.Products.Add(new Entities.Product() { Id = 3, Name = "Monitor", Price = 59.99M, Inventory = 100 });
                dbContext.SaveChanges();
            }
        }
    }
}
