using BuildingBlocks.CQRS;

namespace Ordering.Api.Endpoints
{
    //public record GetOrdersByNameRequest(string OrderName);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/{orderName}", async (string orderName, ISender sender) =>
                {
                    var query = new GetOrdersByNameQuery(orderName);
                    var result = await sender.Send(query);
                    return Results.Ok(result.Adapt<GetOrdersByNameResponse>());
                })
            .WithName("GetOrdersByName")
            .WithDescription("Get Orders by name")
            .WithSummary("Get Orders by name")
            .Produces<GetOrdersResponse>(statusCode: StatusCodes.Status200OK)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
