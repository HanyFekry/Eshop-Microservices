
namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public OrderName OrderName { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CustomerId CustomerId { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => _orderItems.Sum(x => x.Quantity * x.Price);
            private set { }
        }

        public static Order Create(OrderName orderName, Address shippingAddress, Address billingAddress,
            CustomerId customerId, Payment payment)
        {
            var order = new Order
            {
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                CustomerId = customerId,
                Payment = payment,
                Status = OrderStatus.Pending
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress,
            CustomerId customerId, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            CustomerId = customerId;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            var item = new OrderItem(productId, Id, quantity, price);
            _orderItems.Add(item);
        }

        public void Remove(ProductId productId)
        {
            var item = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                _orderItems.Remove(item);
        }

    }
}
