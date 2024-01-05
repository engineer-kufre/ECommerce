using ECommerce.Api.Products.DTOs;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto>? Products, string? ErrorMessage)> GetProductsAsync();

        Task<(bool IsSuccess, ProductDto? Product, string? ErrorMessage)> GetProductAsync(int id);
    }
}
