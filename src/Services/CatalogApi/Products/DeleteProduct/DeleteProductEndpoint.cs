

namespace CatalogApi.Products.DeleteProduct
{
    public record DeleteProductResponse(bool Success);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
