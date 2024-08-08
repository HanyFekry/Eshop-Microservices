

namespace CatalogApi.Products.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IReadOnlyList<ProductDto> ProductDtos);
    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().ToListAsync();
            return new GetProductsResult(result.Adapt<IReadOnlyList<ProductDto>>());
        }
    }
}
