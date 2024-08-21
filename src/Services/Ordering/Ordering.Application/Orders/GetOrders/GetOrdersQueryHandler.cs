namespace Ordering.Application.Orders.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            int pageIndex = request.PaginatedRequest.PageIndex;
            int pageSize = request.PaginatedRequest.PageSize;
            long totalCount = await context.Orders.LongCountAsync(cancellationToken);
            var ordersFromDb = await context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount,
                ordersFromDb.ToOrderDtoList().ToList()
            ));
        }
    }
}
