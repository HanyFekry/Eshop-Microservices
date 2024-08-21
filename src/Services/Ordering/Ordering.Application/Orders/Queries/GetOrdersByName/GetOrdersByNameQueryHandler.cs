
namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {

            var ordersFromDb = await context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.OrderName.Value.Contains(request.OrderName))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return new GetOrdersByNameResult(ordersFromDb.ToOrderDtoList().ToList());

        }
    }
}
