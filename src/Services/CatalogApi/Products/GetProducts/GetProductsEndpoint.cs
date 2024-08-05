

namespace CatalogApi.Products.GetProducts
{
    //public record GetProductsRequest();
    public record GetProductsResponse(IReadOnlyList<ProductDto> ProductDtos);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet($"/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                return Results.Ok(result.Adapt<GetProductsResponse>());
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
                .WithDescription("Get all products")
                .WithSummary("Get products");
        }
    }
}
