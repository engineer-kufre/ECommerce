using ECommerce.Api.Customers.DataAccess;
using ECommerce.Api.Customers.DTOs;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
