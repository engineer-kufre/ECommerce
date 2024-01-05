using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsService.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsService.GetProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound();
        }
    }
}
