
namespace Ordering.Api.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);

    public record CreateOrderResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateOrderResult>();
                return Results.Created($"/orders/{response.Id}", response);

            })
            .WithName("CreateOrder")
            .WithDescription("Create Order")
            .WithSummary("Create Order")
            .Produces<CreateOrderResponse>(statusCode: StatusCodes.Status201Created)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
