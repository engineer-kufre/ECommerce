using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);

            if (ordersResult.IsSuccess)
            {
                if (ordersResult.Orders != null && ordersResult.Orders.Any()
                    && productResult.Products != null && productResult.Products.Any())
                {
                    foreach (var order in ordersResult.Orders)
                    {
                        foreach (var item in order.Items)
                        {
                            item.ProductName = productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "";
                        }
                    }
                }

                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new { Name = "Customer info is not available" },
                    ordersResult.Orders
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
