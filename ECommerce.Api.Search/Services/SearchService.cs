using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public SearchService(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();

            if (ordersResult.IsSuccess)
            {
                if (ordersResult.Orders != null && ordersResult.Orders.Any()
                    && productResult.Products != null && productResult.Products.Any())
                {
                    foreach (var order in ordersResult.Orders)
                    {
                        foreach (var item in order.Items)
                        {
                            item.ProductName = productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                        }
                    }
                }

                var result = new
                {
                    ordersResult.Orders
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
