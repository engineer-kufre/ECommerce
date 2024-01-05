using AutoMapper;
using ECommerce.Api.Customers.DataAccess;
using ECommerce.Api.Customers.DTOs;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;

        public CustomerService(CustomerDbContext dbContext, ILogger<CustomerService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            this.SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                var customers = new List<Customer>()
                {
                    new Customer(){Id = 1, Name = "Dan Brown", Address = "2, Trucker Avenue, Down Town"},
                    new Customer(){Id = 2, Name = "James Brown", Address = "2, Trucker Avenue, Down Town"},
                    new Customer(){Id = 3, Name = "Issac Brown", Address = "2, Trucker Avenue, Down Town"},
                };

                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, CustomerDto? Customer, string? ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null)
                {
                    var result = mapper.Map<Customer, CustomerDto>(customer);

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

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto>? Customers, string? ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);

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
