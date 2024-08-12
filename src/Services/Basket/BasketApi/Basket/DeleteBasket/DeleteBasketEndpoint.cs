namespace BasketApi.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool Success);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}", async (string userName, ISender sender) =>
                {
                    var command = new DeleteBasketCommand(userName);
                    var result = await sender.Send(command);
                    return Results.Ok(result.Adapt<DeleteBasketResponse>());
                })
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound)
                .WithDescription("Delete Basket")
                .WithSummary("Delete Basket");
        }
    }
}
