using ECommerce.Api.Orders.DTOs;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto>? Orders, string? ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
