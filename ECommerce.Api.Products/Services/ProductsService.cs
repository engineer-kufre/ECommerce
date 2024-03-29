﻿using AutoMapper;
using ECommerce.Api.Products.DataAccess;
using ECommerce.Api.Products.DTOs;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                    new Product(){Id = 2, Name = "Mouse", Price = 5, Inventory = 1000},
                    new Product(){Id = 3, Name = "Monitor", Price = 150, Inventory = 100},
                    new Product(){Id = 4, Name = "CPU", Price = 200, Inventory = 50}
                };

                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductDto>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductDto? Product, string? ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Product, ProductDto>(product);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
