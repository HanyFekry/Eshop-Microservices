
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderFromDb = await context.Orders.FindAsync(OrderId.Of(request.OrderId), cancellationToken);
            if (orderFromDb == null)
                throw new OrderNotFoundException(request.OrderId);
            context.Orders.Remove(orderFromDb);
            await context.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
