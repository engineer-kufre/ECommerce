using AutoMapper;
using ECommerce.Api.Orders.DataAccess;
using ECommerce.Api.Orders.DTOs;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderService> logger;
        private readonly IMapper mapper;

        public OrderService(OrderDbContext dbContext, ILogger<OrderService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            this.SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                var orders = new List<Order>()
                {
                    new Order(){
                        Id = 1,
                        CustomerId = 1,
                        OrderDate = DateTime.Now,
                        Items = new List<OrderItem>()
                        {
                            new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                        }
                    },
                    new Order()
                    {
                        Id = 2,
                        CustomerId = 1,
                        OrderDate = DateTime.Now.AddDays(-1),
                        Items = new List<OrderItem>()
                        {
                            new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                        }
                    },
                    new Order()
                    {
                        Id = 3,
                        CustomerId = 2,
                        OrderDate = DateTime.Now,
                        Items = new List<OrderItem>()
                        {
                            new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                            new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                        }
                    }
                };

                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderDto>? Orders, string? ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

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
