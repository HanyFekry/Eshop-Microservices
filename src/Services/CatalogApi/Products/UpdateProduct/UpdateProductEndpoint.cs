
namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, List<string> Category, double Price,
        string ImageUrl);
    public record UpdateProductResponse(bool Success);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }
}
