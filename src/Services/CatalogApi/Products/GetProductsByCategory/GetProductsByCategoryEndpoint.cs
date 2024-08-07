using CatalogApi.Dtos;
using CatalogApi.Products.GetProducts;

namespace CatalogApi.Products.GetProductsByCategory
{
    //public record GetProductsByCategoryRequest(string category);
    public record GetProductsByCategoryResponse(IReadOnlyList<ProductDto> ProductDtos);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductsByCategoryQuery(category);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsByCategoryResponse>();//new GetProductsByCategoryResponse(result.ProductDtos);
                return Results.Ok(response);
            })
                .WithName("GetProductsByCategory")
                .Produces<GetProductsResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound)
                .WithDescription("Get products by category")
                .WithSummary("Get products by category");
        }
    }
}
