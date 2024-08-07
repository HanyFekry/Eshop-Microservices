using CatalogApi.Products.GetProducts;

namespace CatalogApi.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string category)
        : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IReadOnlyList<ProductDto> ProductDtos);
    public class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryHandler.Handle invoked");
            var result = await session.Query<Product>().Where(x => x.Category.Contains(request.category)).ToListAsync();
            return new GetProductsByCategoryResult(result.Adapt<IReadOnlyList<ProductDto>>());
        }
    }
}
