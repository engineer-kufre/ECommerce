using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DataAccess
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
