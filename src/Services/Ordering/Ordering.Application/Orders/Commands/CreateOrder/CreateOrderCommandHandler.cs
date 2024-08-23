
namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await GenerateOrder(request.Order);
            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);

        }

        private async Task<Order> GenerateOrder(OrderDto dto)
        {
            //var dto = command.Order;
            var billing = dto.BillingAddress;
            var shipping = dto.ShippingAddress;
            var pay = dto.Payment;
            var shippingAddress = Address.Of(shipping.FirstName, shipping.LastName, shipping.Email, shipping.Street,
                shipping.Country, shipping.City, shipping.PostalCode);
            var billingAddress = Address.Of(billing.FirstName, billing.LastName, billing.Email, billing.Street,
                billing.Country, billing.City, billing.PostalCode);
            var payment = Payment.Of(pay.CardName, pay.CardNumber, pay.Expiration, pay.Cvv, pay.PaymentMethod);
            Order order = Order.Create(OrderId.Of(dto.Id), CustomerId.Of(dto.CustomerId), OrderName.Of(dto.OrderName)
                , shippingAddress, billingAddress, payment);

            foreach (var item in dto.OrderItems)
            {
                order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            return order;

        }
    }
}
