
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderFromDb = await context.Orders.FindAsync(OrderId.Of(request.Order.Id));
            if (orderFromDb == null)
                throw new OrderNotFoundException(request.Order.Id);
            var order = await UpdateOrder(orderFromDb, request.Order);
            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }
        private async Task<Order> UpdateOrder(Order order, OrderDto dto)
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
            order.Update(OrderId.Of(dto.Id), CustomerId.Of(dto.CustomerId), OrderName.Of(dto.OrderName)
                , shippingAddress, billingAddress, payment, dto.Status);

            return order;

        }
    }
}
