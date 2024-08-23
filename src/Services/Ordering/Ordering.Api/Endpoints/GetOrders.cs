using Mapster;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.Api.Endpoints
{
    public record GetOrdersRequest(PaginatedRequest PaginatedRequest);

    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders",
                    async ([AsParameters] PaginatedRequest request, ISender sender) =>
                    {
                        var query = new GetOrdersQuery(request);
                        //var query = request.Adapt<GetOrdersQuery>();
                        var result = await sender.Send(query);
                        return Results.Ok(new GetOrdersResponse(result.Orders));
                    })
                .WithName("GetOrders")
                .WithDescription("Get Orders")
                .WithSummary("Get Orders")
                .Produces<GetOrdersResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
