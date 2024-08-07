﻿
namespace CatalogApi.Products.GetProductById
{
    public record GetProductByIdResponse(Guid Id, string Name, string Description, List<string> Category, double Price);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Get Product By Id")
                .WithSummary("Get Product By Id");
        }
    }
}
