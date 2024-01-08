namespace ECommerce.Api.Search.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = "Product information is not available";

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
