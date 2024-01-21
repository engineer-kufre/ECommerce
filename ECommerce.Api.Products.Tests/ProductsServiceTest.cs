using AutoMapper;
using ECommerce.Api.Products.DataAccess;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;

            var dbContext = new ProductsDbContext(options);

            var numberOfProducts = 10;

            CreateProducts(dbContext, numberOfProducts);

            var productProfile = new ProductProfile();

            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(configuration);

            var productService = new ProductsService(dbContext, null, mapper);

            var response = await productService.GetProductsAsync();

            Assert.True(response.IsSuccess);
            Assert.True(response.Products != null && response.Products.Any());
            Assert.Equal(numberOfProducts, response.Products.Count());
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId)).Options;

            var dbContext = new ProductsDbContext(options);

            var numberOfProducts = 10;

            CreateProducts(dbContext, numberOfProducts);

            var productProfile = new ProductProfile();

            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(configuration);

            var productService = new ProductsService(dbContext, null, mapper);

            var response = await productService.GetProductAsync(1);

            Assert.True(response.IsSuccess);
            Assert.NotNull(response.Product);
            Assert.True(response.Product.Id == 1);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsNoProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductReturnsNoProductUsingInvalidId)).Options;

            var dbContext = new ProductsDbContext(options);

            var numberOfProducts = 10;

            CreateProducts(dbContext, numberOfProducts);

            var productProfile = new ProductProfile();

            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(configuration);

            var productService = new ProductsService(dbContext, null, mapper);

            var response = await productService.GetProductAsync(-1);

            Assert.False(response.IsSuccess);
            Assert.Null(response.Product);
            Assert.NotNull(response.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext, int numberOfProducts)
        {
            for (int i = 1; i <= numberOfProducts; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14),
                });
            }

            dbContext.SaveChanges();
        }
    }
}