using BasketApi.Dtos;

namespace BasketApi.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto Basket);

    public record CheckoutBasketResponse(bool Success);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("checkout")
            .Produces<CheckoutBasketResponse>(statusCode: StatusCodes.Status200OK)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
            .WithDescription("checkout Basket")
            .WithSummary("checkout Basket");
        }
    }
}
