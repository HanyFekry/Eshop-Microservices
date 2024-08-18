
namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(ProductId productId, OrderId orderId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
            Price = price;
        }
        public ProductId ProductId { get; set; }
        public OrderId OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
