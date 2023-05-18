using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Entities;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
    public class ProductTests
    {
        [Fact]
        public async Task GetProductAndReturnProductByValidId()
        {
            //set dbcontext for unit test
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductAndReturnProductByValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            //seeding data for test
            CreateProductsForTest(dbContext);

            //set Dtos using automapper
            var profile = new ProductsProfile();
            var config = new MapperConfiguration(c => c.AddProfile(profile));
            var mapper = new Mapper(config);

            //calling products provider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await productsProvider.GetAllAsync();

            Assert.True(products.isSuccess);
            Assert.True(products.productsDto.Any());
            Assert.Null(products.errorMsg);
        }

        [Fact]
        public async Task GetProductsAndReturnAllProducts()
        {
            //set dbcontext for unit test
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsAndReturnAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);

            //seeding data for test
            CreateProductsForTest(dbContext);

            //set Dtos using automapper
            var profile = new ProductsProfile();
            var config = new MapperConfiguration(c => c.AddProfile(profile));
            var mapper = new Mapper(config);

            //calling products provider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductByIdAsync(1);

            Assert.True(product.isSuccess);
            Assert.NotNull(product.productDto);
            Assert.True(product.productDto.Id == 1);
            Assert.Null(product.errorMsg);
        }

        [Fact]
        public async Task GetProductAndReturnProductByInvalidId()
        {
            //set dbcontext for unit test
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductAndReturnProductByInvalidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            //seeding data for test
            CreateProductsForTest(dbContext);

            //set Dtos using automapper
            var profile = new ProductsProfile();
            var config = new MapperConfiguration(c => c.AddProfile(profile));
            var mapper = new Mapper(config);

            //calling products provider
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductByIdAsync(-1);

            Assert.False(product.isSuccess);
            Assert.Null(product.productDto);
            Assert.NotNull(product.errorMsg);
        }

        private void CreateProductsForTest(ProductsDbContext dbContext)
        {
            if(!dbContext.Products.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    dbContext.Products.Add(new Product() { Id = i, Name = Guid.NewGuid().ToString(), Inventory = i + 10, Price = (decimal)(i * 3.14) });
                }
                dbContext.SaveChanges();
            }
        }
    }
}
