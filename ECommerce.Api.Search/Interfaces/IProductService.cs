using ECommerce.Api.Search.DTOs;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto>? Products, string? ErrorMessage)> GetProductsAsync();
    }
}
