using CatalogApi.Products.GetProducts;

namespace CatalogApi.Products.GetProductById
{
    public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(ProductDto? Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:Guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);//request.Adapt<GetProductByIdQuery>();
                var result = await sender.Send(query);
                if (result.Product != null)
                {
                    var response = result.Adapt<GetProductByIdResponse>();
                    return Results.Ok(response);
                }
                return Results.NotFound();
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(statusCode: StatusCodes.Status200OK)
                .ProducesProblem(statusCode: StatusCodes.Status404NotFound)
                .WithDescription("Get Product By Id")
                .WithSummary("Get Product By Id");
        }
    }
}
