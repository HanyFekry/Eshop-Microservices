
namespace Ordering.Api.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);

    public record UpdateOrderResponse(bool Success);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResult>();
                return Results.Ok(response);

            })
            .WithName("UpdateOrder")
            .WithDescription("Update Order")
            .WithSummary("Update Order")
            .Produces<UpdateOrderResponse>(statusCode: StatusCodes.Status200OK)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
            .ProducesProblem(statusCode: StatusCodes.Status404NotFound);
        }
    }
}
