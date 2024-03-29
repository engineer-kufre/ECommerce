﻿using ECommerce.Api.Customers.DTOs;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, IEnumerable<CustomerDto>? Customers, string? ErrorMessage)> GetCustomersAsync();

        Task<(bool IsSuccess, CustomerDto? Customer, string? ErrorMessage)> GetCustomerAsync(int id);
    }
}
