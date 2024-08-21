
namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IReadOnlyList<OrderDto> Orders);

}
