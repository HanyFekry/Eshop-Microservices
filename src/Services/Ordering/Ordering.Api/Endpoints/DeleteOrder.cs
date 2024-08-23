
namespace Ordering.Api.Endpoints
{
    //public record DeleteOrderRequest(OrderDto Order);

    public record DeleteOrderResponse(bool Success);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
                {
                    var command = new DeleteOrderCommand(id);
                    var result = await sender.Send(command);
                    var response = result.Adapt<DeleteOrderResult>();
                    return Results.Ok(response);

                })
                .WithName("DeleteOrder")
                .WithDescription("Delete Order")
                .WithSummary("Delete Order")
                .Produces<DeleteOrderResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound);
        }
    }
}