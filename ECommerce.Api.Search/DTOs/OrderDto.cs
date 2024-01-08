namespace ECommerce.Api.Search.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
