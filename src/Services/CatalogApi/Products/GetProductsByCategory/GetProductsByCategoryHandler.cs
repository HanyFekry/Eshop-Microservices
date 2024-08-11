
namespace CatalogApi.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category)
        : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IReadOnlyList<ProductDto> ProductDtos);
    public class GetProductsByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().Where(x => x.Category.Contains(request.Category)).ToListAsync(cancellationToken);
            return new GetProductsByCategoryResult(result.Adapt<IReadOnlyList<ProductDto>>());
        }
    }
}
