using ECommerce.Api.Products.DataAccess;
using ECommerce.Api.Products.DTOs;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
