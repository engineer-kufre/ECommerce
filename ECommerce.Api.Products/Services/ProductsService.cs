using AutoMapper;
using ECommerce.Api.Products.DataAccess;
using ECommerce.Api.Products.DTOs;
using ECommerce.Api.Products.Interfaces;

namespace ECommerce.Api.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsService> logger;
        private readonly IMapper mapper;

        public ProductsService(ProductsDbContext dbContext, ILogger<ProductsService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            this.SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product(){Id = 1, Name = "Keyboard", Price = 20, Inventory = 1000},
                    new Product(){Id = 1, Name = "Mouse", Price = 5, Inventory = 1000},
                    new Product(){Id = 1, Name = "Monitor", Price = 150, Inventory = 100},
                    new Product(){Id = 1, Name = "CPU", Price = 200, Inventory = 50}
                };

                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }

        public Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
