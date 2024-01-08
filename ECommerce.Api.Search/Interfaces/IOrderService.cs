using ECommerce.Api.Search.DTOs;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto>? Orders, string? ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
