namespace Ordering.Api.Endpoints
{
    public record GetOrdersByCustomerIdResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomerId : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
                {
                    var query = new GetOrdersByCustomerIdQuery(customerId);
                    var result = await sender.Send(query);
                    return Results.Ok(result.Adapt<GetOrdersByCustomerIdResponse>());
                })
                .WithName("GetOrderByCustomerId")
                .WithDescription("Get Order By CustomerId")
                .WithSummary("Get Order By CustomerId")
                .Produces<GetOrdersByCustomerIdResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
