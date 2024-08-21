using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByCustomerIdQuery, GetOrdersByCustomerIdResult>
    {
        public async Task<GetOrdersByCustomerIdResult> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var ordersFromDb = await context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Where(x => x.CustomerId.Value == request.CustomerId)
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerIdResult(ordersFromDb.ToOrderDtoList().ToList());
        }
    }
}
